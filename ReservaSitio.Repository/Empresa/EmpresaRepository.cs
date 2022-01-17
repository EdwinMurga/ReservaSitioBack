using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.Empresa;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
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

namespace ReservaSitio.Repository.Empresa
{
    public class EmpresaRepository : BaseRepository,IEmpresaRepository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public EmpresaRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }


        public Task<ResultDTO<EmpresaDTO>> DeleteEmpresa(EmpresaDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<EmpresaDTO>> GetEmpresa(EmpresaDTO request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            EmpresaDTO item = new EmpresaDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_empresa", request.iid_empresa);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<EmpresaDTO>("[dbo].[SP_EMPRESA_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (EmpresaDTO)query.FirstOrDefault();
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

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<EmpresaDTO>> GetListEmpresa(EmpresaDTO request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            List<EmpresaDTO> list = new List<EmpresaDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_empresa", request.iid_empresa);
                parameters.Add("@p_vnombre_completo", request.vnombre_completo);
                parameters.Add("@p_vruc", request.vruc);
                parameters.Add("@p_iid_distrito", request.iid_distrito);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<EmpresaDTO>)cn.Query<EmpresaDTO>("[dbo].[SP_EMPRESA_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<EmpresaDTO>> RegisterEmpresa(EmpresaDTO request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_empresa", request.iid_empresa);
                        parameters.Add("@p_vnombre_imagen_logo", request.vnombre_imagen_logo);
                        parameters.Add("@p_vnombre_completo", request.vnombre_completo);
                        parameters.Add("@p_vruc", request.vruc);
                        parameters.Add("@p_vdireccion", request.vdireccion);
                        parameters.Add("@p_vurl", request.vurl);
                        parameters.Add("@p_correo_electronico", request.correo_electronico);
                        parameters.Add("@p_vtelefono", request.vtelefono);
                        parameters.Add("@p_iid_distrito", request.iid_distrito);

                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_EMPRESA_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
    }
}
