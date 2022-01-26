using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
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

namespace ReservaSitio.Repository.ParametrosAplicacion
{
    public class ParametroRepository: BaseRepository,IParametroRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public ParametroRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        public async Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_parametro", request.iid_parametro);
                     
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PARAMETRO_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                            }
                        }
                        res.IsSuccess = (res.Codigo == 0 ? false : true);
                        res.Message = (res.Codigo == 0 ? UtilMensajes.strInformnacionNoElimina : UtilMensajes.strInformnacionEliminada);
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

        public async Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            List<ParametroAplicacionDTO> list = new List<ParametroAplicacionDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_parametro", request.iid_parametro);
                parameters.Add("@p_vdescripcion", request.vdescripcion);
                parameters.Add("@p_vvalor_cadena", request.vvalor_cadena);
                //parameters.Add("@p_ivalor_entero", request.ivalor_entero);
                //parameters.Add("@p_nvalor_decimal", request.nvalor_decimal);
                parameters.Add("@p_iid_empresa", request.iid_empresa);

                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ParametroAplicacionDTO>)cn.Query<ParametroAplicacionDTO>("[dbo].[SP_PARAMETRO_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            ParametroAplicacionDTO item = new ParametroAplicacionDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_parametro", request.iid_parametro);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<ParametroAplicacionDTO>("[dbo].[SP_PARAMETRO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (ParametroAplicacionDTO)query.FirstOrDefault();
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
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_parametro", request.iid_parametro);
                        parameters.Add("@p_vdescripcion", request.vdescripcion);
                        parameters.Add("@p_vvalor_cadena", request.vvalor_cadena);
                        parameters.Add("@p_ivalor_entero", request.ivalor_entero);
                        parameters.Add("@p_nvalor_decimal", request.nvalor_decimal);                    
                        parameters.Add("@p_iid_empresa", request.iid_empresa);        

                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PARAMETRO_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
