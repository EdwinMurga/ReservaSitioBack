using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ReservaSitio.Entities.Enum;

namespace ReservaSitio.Repository.Usuario
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        public  UsuarioRepository(ICustomConnection connection, IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
        
        public async Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_usuario", request.iid_usuario);
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                parameters.Add("@p_iid_tipo_documento", request.iid_tipo_documento);
                parameters.Add("@p_vnro_documento", request.vnro_documento);
                parameters.Add("@p_vnombres", request.vnombres);
                parameters.Add("@p_vapellido_paterno", request.vapellido_paterno);
                parameters.Add("@p_vapellido_materno", request.vapellido_materno);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registro_ini);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registro_fin);

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = await mConnection.BeginConnection(true))
                {
                    list = (List<UsuarioDTO>)cn.Query<UsuarioDTO>("[dbo].[SP_USUARIO_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = (list.ToList().Count > 0 ? true : false);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
                res.data = list.ToList();
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }

        public async Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            UsuarioDTO item = new UsuarioDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query =await cn.QueryAsync<UsuarioDTO>("[dbo].[SP_USUARIO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (UsuarioDTO)query.FirstOrDefault();
                    res.IsSuccess = (query.Any() == true ? true : false);
                }
                // await mConnection.Complete();
                res.Message = (res.IsSuccess ? UtilMensajes.strInformnacionGrabada : UtilMensajes.strInformnacionNoEncontrada);             
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }

        public async Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = new SqlConnection(_connectionString))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);
                        parameters.Add("@p_iid_perfil", request.iid_perfil);
                        parameters.Add("@p_iid_tipo_documento", request.iid_tipo_documento);
                        parameters.Add("@p_vnro_documento", request.vnro_documento);
                        parameters.Add("@p_vnombres", request.vnombres);
                        parameters.Add("@p_vapellido_paterno", request.vapellido_paterno);
                        parameters.Add("@p_vapellido_materno", request.vapellido_materno);
                        parameters.Add("@p_vcorreo_electronico", request.vcorreo_electronico);
                        parameters.Add("@p_vnumero_telefonico", request.vnumero_telefonico);
                        parameters.Add("@p_vclave", request.vclave);
                        parameters.Add("@p_dfecha_caduca_clave", request.dfecha_caduca_clave);
                        parameters.Add("@p_dfecha_cambio_clave", request.dfecha_cambio_clave);
                        parameters.Add("@p_cantidad_intentos", request.cantidad_intentos);
                        parameters.Add("@p_iid_indica_bloqueo", request.iid_indica_bloqueo);
                        parameters.Add("@p_dfecha_ultimo_acceso", request.dfecha_ultimo_acceso);
                        parameters.Add("@p_iid_empresa", request.iid_empresa);


                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);



                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                res.IsSuccess = true;
                                res.Message = UtilMensajes.strInformnacionGrabada;
                            }
                        }
                        await mConnection.Complete();
                    }


                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();
                }
            }
            return res;
        }
    }
}



