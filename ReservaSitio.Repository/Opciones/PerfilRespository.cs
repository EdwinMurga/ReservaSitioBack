using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.Perfiles;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
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

namespace ReservaSitio.Repository.Opcion
{
    public class PerfilRespository : BaseRepository, IPerfilRespository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public  PerfilRespository(ICustomConnection connection
            , IConfiguration configuration
             , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio"); 
        }
        public async Task<ResultDTO<PerfilDTO>> DeletePerfil(PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_vcodigo_cliente", request.vdescripcion_perfil);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["iid"].ToString());
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

        public async Task<ResultDTO<PerfilDTO>> GetListPerfil(PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            List<PerfilDTO> list = new List<PerfilDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                parameters.Add("@p_vnombre_perfil", request.vnombre_perfil);
                parameters.Add("@p_vdescripcion_perfil", request.vdescripcion_perfil);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<PerfilDTO>)cn.Query<PerfilDTO>("[dbo].[SP_PERFIL_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = (list.ToList().Count > 0 ? true : false);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
                res.data = list.ToList();
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoEncontrada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PerfilDTO>> GetPerfil(PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            PerfilDTO item = new PerfilDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                using (var cn = new SqlConnection(_connectionString))
                {
                    var query = await cn.QueryAsync<PerfilDTO>("[dbo].[SP_PERFIL_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PerfilDTO)query.FirstOrDefault();
                }
               // await mConnection.Complete();
                res.IsSuccess = (item!=null?true:false) ;
                res.Message = (item!= null ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada) ;
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoEncontrada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PerfilDTO>> RegisterPerfil(PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_perfil", request.iid_perfil);
                        parameters.Add("@p_vnombre_perfil", request.vnombre_perfil);
                        parameters.Add("@p_vdescripcion_perfil", request.vdescripcion_perfil);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PERFIL_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = request.iid_usuario_registra;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                      await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
    }
}
