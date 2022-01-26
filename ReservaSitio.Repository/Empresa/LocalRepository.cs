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
    public class LocalRepository : BaseRepository, ILocalRepository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public LocalRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        public async Task<ResultDTO<LocalDTO>> DeleteLocal(LocalDTO request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_local", request.iid_local);
                       parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_LOCAL_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

        public async Task<ResultDTO<LocalDTO>> GetListLocal(LocalDTO request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            List<LocalDTO> list = new List<LocalDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_local", request.iid_local);
                parameters.Add("@p_vdescripcion", request.vdescripcion);
                parameters.Add("@p_icantidad_pisos", request.icantidad_pisos);
                parameters.Add("@p_iid_distrito", request.iid_distrito);
                parameters.Add("@p_iid_empresa", request.iid_empresa);   

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<LocalDTO>)cn.Query<LocalDTO>("[dbo].[SP_LOCAL_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<ResultDTO<LocalDTO>> GetLocal(LocalDTO request)
        {
                ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            LocalDTO item = new LocalDTO();

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_local", request.iid_local);
                    using (var cn = new SqlConnection(_connectionString))
                    {
                        var query = await cn.QueryAsync<LocalDTO>("[dbo].[SP_LOCAL_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                        item = (LocalDTO)query.FirstOrDefault();
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
        public async Task<ResultDTO<LocalDTO>> RegisterLocal(LocalDTO request)
        {
                ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                            var parameters = new DynamicParameters();
                            parameters.Add("@p_iid_local", request.iid_local);
                            parameters.Add("@p_vdescripcion", request.vdescripcion);
                            parameters.Add("@p_vdireccion", request.vdireccion);
                            parameters.Add("@p_icantidad_pisos", request.icantidad_pisos);
                            parameters.Add("@p_iid_distrito", request.iid_distrito);
                            parameters.Add("@p_iid_empresa", request.iid_empresa);
                            parameters.Add("@p_iid_registro", request.iid_usuario_registra);           


                          //  parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                           // parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                            using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_LOCAL_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
