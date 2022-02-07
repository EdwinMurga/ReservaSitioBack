using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.Repository.Base;
using System;
using System.Collections;
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
   public  class PerfilOpcionRepository: BaseRepository, IPerfilOpcionRepository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        private readonly ILogErrorTablaRepository iILogErrorTablaRepository;
        public  PerfilOpcionRepository(ICustomConnection connection
            , IConfiguration configuration
               , ILogErrorRepository ILogErrorRepository
            , ILogErrorTablaRepository ILogErrorTablaRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            this.iILogErrorTablaRepository = ILogErrorTablaRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
      

        public async Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO> request)
            {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        foreach (PerfilOpcionDTO x in request) {
                            var parameters = new DynamicParameters();
                            parameters.Add("@p_iid_perfil_opcion", x.iid_perfil_opcion);
                            parameters.Add("@p_iid_perfil", x.iid_perfil);
                            parameters.Add("@p_iid_opcion", x.iid_opcion);
                            parameters.Add("@p_iacceso_crear", x.iacceso_crear);
                            parameters.Add("@p_iacceso_actualizar", x.iacceso_actualizar);
                            parameters.Add("@p_iacceso_eliminar", x.iacceso_eliminar);
                            parameters.Add("@p_iacceso_visualizar", x.iacceso_visualizar);
                            parameters.Add("@p_iid_estado_registro", x.iid_estado_registro);
                            parameters.Add("@p_iid_usuario_registra", x.iid_usuario_registra);

                            using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PERFIL_OPCION_REGISTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                            {
                                while (lector.Read())
                                {
                                    res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                    res.IsSuccess = true;
                                    res.Message = UtilMensajes.strInformnacionGrabada;
                                }
                            }

                        }
                       
                        await mConnection.Complete();
                    }


                    scope.Complete();

                    LogErrorTablaDTO req_log = new LogErrorTablaDTO();
                    req_log.iid_usuario_registra = request[0].iid_usuario_registra;
                    req_log.vaccion = "inset/upd";
                    req_log.vnombretabla = this.ToString();

                    //await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
            }
        public async Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request)
            {
                ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = new SqlConnection(_connectionString))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PERFIL_OPCION_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    req_log.vnombretabla = this.ToString();

                    await this.iILogErrorTablaRepository.RegisterLogTablaError(req_log);
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
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
            }
        public async Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            PerfilOpcionDTO item = new PerfilOpcionDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<PerfilOpcionDTO>("[dbo].[SP_PERFIL_OPCION_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PerfilOpcionDTO)query.FirstOrDefault();
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
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }
        public async Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            List<PerfilOpcionDTO> list = new List<PerfilOpcionDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                parameters.Add("@p_iid_opcion", request.iid_opcion);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {

                    list = (List<PerfilOpcionDTO>)cn.Query<PerfilOpcionDTO>("[dbo].[SP_PERFIL_OPCION_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
        {
            ResultDTO<PerfilUsuarioMenu> res = new ResultDTO<PerfilUsuarioMenu>();
            PerfilUsuarioMenu item = new PerfilUsuarioMenu();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_usuario", request.iid_usuario);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<PerfilUsuarioResponseDTO>("[dbo].[SP_PERFIL_OPCION_USUARIO_BY_USUARIO_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var  resulquery = query.ToList();
                    res.IsSuccess = (query.Any() == true ? true : false);
                    if (res.IsSuccess) {
                        List<Menu> lsmodulo = new List<Menu>();
                        List<PerfilOpcionDTO> lsPerOpcion = new List<PerfilOpcionDTO>();
                        List<SubMenu> lsopcion = null;
                        Menu modulo = null;
                        // PerfilOpcionDTO PerOpcio = null;
                        foreach (PerfilUsuarioResponseDTO x in resulquery)
                        {
                            // PerOpcio = new PerfilOpcionDTO();
                            //.iid_opcion = x.iid_opcion;
                            //PerOpcio.iid_perfil = x.iid_perfil;
                            //PerOpcio.iid_perfil_opcion = x.iid_perfil_opcion;

                            //PerOpcio.iacceso_actualizar = x.iacceso_actualizar;
                            //PerOpcio.iacceso_crear = x.iacceso_crear;
                            //PerOpcio.iacceso_eliminar = x.iacceso_eliminar;
                            //PerOpcio.iacceso_visualizar = x.iacceso_visualizar;

                            if (lsmodulo.Find(xx => xx.iid_modulo == x.iid_modulo) == null)
                            {
                                modulo = new Menu();
                                lsopcion = new List<SubMenu>();
                                modulo.iid_modulo = x.iid_modulo;
                                // modulo.iorden = x.iorden_modulo;
                                modulo.text = x.vtitulo_modulo;
                                modulo.link = x.vurl_modulo;
                                modulo.icon = x.vicono_modulo;

                                modulo.iindica_visible_modulo = x.iindica_visible_modulo;
                                // modulo.iid_sistema = x.iid_sistema;
                                if (modulo.iid_modulo == 0) 
                                { 
                                    lsmodulo.Add(modulo);
                                }
                                else { 
                                      
                                foreach (PerfilUsuarioResponseDTO y in resulquery)
                                {
                                    if (lsopcion.Find(xx => xx.iid_opcion == y.iid_opcion && xx.iid_modulo == y.iid_modulo) == null)
                                    {
                                        SubMenu opcion = new SubMenu();
                                        opcion.iid_modulo = y.iid_modulo;
                                        opcion.iid_opcion = y.iid_opcion;
                                        //opcion.iindica_visible = x.iid;
                                        //opcion.iorden = y.iorden_opcion;
                                        opcion.text = y.vtitulo_opcion;
                                        opcion.icon = y.vicono_opcion;
                                        opcion.iindica_visible_opcion = y.iindica_visible_opcion;
                                        opcion.link = y.vurl_opcion;

                                        opcion.icrear = y.iacceso_crear;
                                        opcion.ivisualizar = y.iacceso_visualizar;
                                        opcion.ieliminar = y.iacceso_eliminar;
                                        opcion.iactualizar = y.iacceso_actualizar;

                                        lsopcion.Add(opcion);
                                        modulo.submenu = lsopcion;
                                    }
                                }
                                lsmodulo.Add(modulo);
                                
                                }
                          

                                
                            }
                            // lsPerOpcion.Add(PerOpcio);
                        }
                        // item.perfilOpcion = lsPerOpcion;
                        item.menu = lsmodulo;
                    }
                   
                }
                //item

               

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
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

       

    }


}
