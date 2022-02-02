using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IRepository;
using ReservaSitio.Abstraction.IRepository.LogError;
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
        private readonly ILogErrorRepository iLogErrorRepository;
        private readonly ILogErrorTablaRepository iILogErrorTablaRepository;
        public  UsuarioRepository(ICustomConnection connection, 
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository
             , ILogErrorTablaRepository ILogErrorTablaRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            this.iILogErrorTablaRepository = ILogErrorTablaRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
        #region "usuario "
        public async Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);     
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "delete";
                    req_log.vnombretabla = "usuario";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoElimina;
                    res.InnerException = e.Message.ToString();

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = request.iid_usuario_registra;
                    lg.iid_opcion = 1;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
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
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registro_ini.ToString());
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registro_fin.ToString());

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
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

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
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
                parameters.Add("@p_iid_usuario", request.iid_usuario);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query =await cn.QueryAsync<UsuarioDTO>("[dbo].[SP_USUARIO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (UsuarioDTO)query.FirstOrDefault();
                    res.IsSuccess = (query.Any() == true ? true : false);
                }
                // await mConnection.Complete();
                res.Message = (res.IsSuccess ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);             
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }
        public async Task<ResultDTO<UsuarioDTO>> GetUsuarioParameter(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            UsuarioDTO item = new UsuarioDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_vnro_documento", request.vnro_documento);
                parameters.Add("@p_vnombres", request.vnombres);
                parameters.Add("@p_vapellido_paterno", request.vapellido_paterno);
                parameters.Add("@p_vapellido_materno", request.vapellido_materno);
                parameters.Add("@p_vcorreo_electronico", request.vcorreo_electronico);

                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<UsuarioDTO>("[dbo].[SP_USUARIO_BY_PARAMETER]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (query.Any() == true ? (UsuarioDTO)query.FirstOrDefault() : null);
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
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
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

                    using (var cn = await mConnection.BeginConnection(true))
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
                       // parameters.Add("@p_dfecha_caduca_clave", request.dfecha_caduca_clave);
                       // parameters.Add("@p_dfecha_cambio_clave", request.dfecha_cambio_clave);
                        parameters.Add("@p_cantidad_intentos", request.cantidad_intentos);
                        parameters.Add("@p_iid_indica_bloqueo", request.iid_indica_bloqueo);
                       // parameters.Add("@p_dfecha_ultimo_acceso", request.dfecha_ultimo_acceso);
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

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "insert/upd";
                    req_log.vnombretabla = "usuario";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = request.iid_usuario_registra;
                    lg.iid_opcion = 1;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
       
        
        #endregion

        #region "usuario Acceso"
        public async Task<ResultDTO<UsuarioDTO>> RegisterUsuarioAcceso(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_ACCESO_INSERTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "insert/upd";
                    req_log.vnombretabla = "usuario accesos";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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

        public async Task<ResultDTO<UsuarioDTO>> RegisterUsuarioIntentoLogeo(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                       
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_INTENTO_LOGEO_ACTUALIZA]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "insert";
                    req_log.vnombretabla = "usuario logeo intento";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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
        #endregion

        #region "usuario  Seguridad"
        public async Task<ResultDTO<UsuarioDTO>> RegisterUsuarioSeguridadInserta(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);
                        parameters.Add("@p_vclave", request.vclave);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_SEGURIDAD_INSERTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "insert";
                    req_log.vnombretabla = "usuario seguridad";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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
        #endregion

        #region "usuario Recuperar Clave"
        public async Task<ResultDTO<UsuarioDTO>> RegisterUsuarioRecuperaClave(UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_usuario", request.iid_usuario);
                        parameters.Add("@p_vtoken", request.vcodToken);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USUARIO_RECUPERA_CLAVE_INSERTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                res.Informacion = lector["token"].ToString();
                                res.IsSuccess = true;
                                res.Message = UtilMensajes.strInformnacionGrabada;
                            }
                        }
                        await mConnection.Complete();
                    }


                    scope.Complete();

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request.iid_usuario_registra;
                    req_log.vaccion = "insert/upd";
                    req_log.vnombretabla = "usuario recupera clave";

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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

        public async Task<ResultDTO<UsuarioRecuperarClave>> GetUsuarioRecuperaClave(UsuarioDTO request)
        {
            ResultDTO<UsuarioRecuperarClave> res = new ResultDTO<UsuarioRecuperarClave>();
            UsuarioRecuperarClave item = new UsuarioRecuperarClave();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_usuario", request.iid_usuario);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<UsuarioRecuperarClave>("[dbo].[SP_USUARIO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (UsuarioRecuperarClave)query.FirstOrDefault();
                    res.IsSuccess = (query.Any() == true ? true : false);
                }
                // await mConnection.Complete();
                res.Message = (res.IsSuccess ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        #endregion
    }
}



