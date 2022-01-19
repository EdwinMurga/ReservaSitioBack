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
    public class TablaParametroRepository : BaseRepository, ITablaParametroRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public TablaParametroRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        #region "Tabla"

        public async Task<ResultDTO<TablaParametroDTO>> RegisterTablaParametro(TablaParametroDTO request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_tabla_auxiliar", request.iid_tabla_auxiliar);
                        parameters.Add("@p_vdescripcion", request.vdescripcion);
                        parameters.Add("@p_iindica_agregacion", request.iindica_agregacion);


                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLA_CABECERA_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

        public async Task<ResultDTO<TablaParametroDTO>> DeleteTablaParametro(TablaParametroDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<TablaParametroDTO>> GetTablaParametro(TablaParametroDTO request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            TablaParametroDTO item = new TablaParametroDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_tabla_auxiliar", request.iid_tabla_auxiliar);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<TablaParametroDTO>("[dbo].[SP_TABLA_CABECERA_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (TablaParametroDTO)query.FirstOrDefault();
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

        public async Task<ResultDTO<TablaParametroDTO>> GetListTablaParametro(TablaParametroDTO request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            List<TablaParametroDTO> list = new List<TablaParametroDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_tabla_auxiliar", request.iid_tabla_auxiliar);
                parameters.Add("@p_vdescripcion", request.vdescripcion);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);



                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<TablaParametroDTO>)cn.Query<TablaParametroDTO>("[dbo].[SP_TABLA_CABECERA_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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


        #endregion



        #region "Tabla Detalle"



        public async Task<ResultDTO<TablaDetalleParametroDTO>> RegisterTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_tabla_detalle", request.iid_tabla_auxiliar);
                        parameters.Add("@p_iid_tabla_auxiliar", request.iid_tabla_auxiliar);
                        parameters.Add("@p_iid_registro_tabla", request.iid_registro_tabla);
                        parameters.Add("@p_iid_codigo_descripcion", request.iid_codigo_descripcion);
                        parameters.Add("@p_vvalor_texto_corto", request.vvalor_texto_corto);
                        parameters.Add("@p_vvalor_texto_largo", request.vvalor_texto_largo);
                        parameters.Add("@p_nvalor_entero", request.nvalor_entero);
                        parameters.Add("@p_nvalor_decimal", request.nvalor_decimal);

                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLA_DETALLE_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

        public async Task<ResultDTO<TablaDetalleParametroDTO>> DeleteTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<TablaDetalleParametroDTO>> GetTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            TablaDetalleParametroDTO item = new TablaDetalleParametroDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_tabla_detalle", request.iid_tabla_auxiliar);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<TablaDetalleParametroDTO>("[dbo].[SP_TABLA_DETALLE_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (TablaDetalleParametroDTO)query.FirstOrDefault();
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

        public async Task<ResultDTO<TablaDetalleParametroDTO>> GetListTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            List<TablaDetalleParametroDTO> list = new List<TablaDetalleParametroDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_tabla_detalle", request.iid_tabla_detalle);
                parameters.Add("@p_iid_tabla_auxiliar", request.iid_tabla_auxiliar);
                parameters.Add("@p_iid_registro_tabla", request.iid_estado_registro);
                parameters.Add("@p_iid_codigo_descripcion", request.iid_codigo_descripcion);
                parameters.Add("@p_vvalor_texto_largo", request.vvalor_texto_largo);
                parameters.Add("@p_vvalor_texto_corto", request.vvalor_texto_corto);

                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);



                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<TablaDetalleParametroDTO>)cn.Query<TablaDetalleParametroDTO>("[dbo].[SP_TABLA_DETALLE_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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


        #endregion

    }
}
