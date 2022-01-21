using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.Opciones;
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
   public class OpcionRepository: BaseRepository, IOpcionRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public OpcionRepository(ICustomConnection connection
            , IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;

            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        #region "usuario"
        public async Task<ResultDTO<OpcionDTO>> RegisterOpcion(OpcionDTO request) {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_opcion", request.iid_opcion);
                        parameters.Add("@p_iid_modulo", request.iid_modulo);
                        parameters.Add("@p_vtitulo", request.vtitulo);
                        parameters.Add("@p_vurl", request.vurl);
                        parameters.Add("@p_iindica_visible", request.iindica_visible);
                        parameters.Add("@p_iorden", request.iorden);
                        parameters.Add("@p_vicono", request.vicono);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_OPCION_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    lg.iid_usuario_registra = 0;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
        public async Task<ResultDTO<OpcionDTO>> DeleteOpcion(OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_vcodigo_cliente", request.iid_opcion);


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

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = 0;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
        public async Task<ResultDTO<OpcionDTO>> GetOpcion(OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            OpcionDTO item = new OpcionDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_opcion", request.iid_opcion);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query =await cn.QueryAsync<OpcionDTO>("[dbo].[SP_OPCION_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item =(OpcionDTO) query.FirstOrDefault();
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }
        public async Task<ResultDTO<OpcionDTO>> GetListOpcion(OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            List<OpcionDTO> list = new List<OpcionDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_opcion", request.iid_opcion);
                parameters.Add("@p_iid_modulo", request.iid_modulo);
                parameters.Add("@p_vtitulo", request.vtitulo);
                parameters.Add("@p_iindica_visible", request.iindica_visible);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);
                using (var cn = new SqlConnection(_connectionString))
                {

                    list = (List<OpcionDTO>)cn.Query<OpcionDTO>("[dbo].[SP_OPCION_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = (list.ToList().Count > 0 ? true : false);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
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

        #endregion

      
    }
}
