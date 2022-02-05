using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.API.DTO;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Perfiles
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class PerfilController : Controller
    {
        private readonly IPerfilAplication iIPerfilAplication;
        private readonly IPerfilOpcionAplication iPerfilOpcionAplication;
        private readonly ILogErrorAplication iLogErrorAplication;

        public PerfilController(IPerfilAplication _iIPerfilAplication
            , IPerfilOpcionAplication IPerfilOpcionAplication
             ,  ILogErrorAplication ILogErrorAplication)
        {
            this.iIPerfilAplication = _iIPerfilAplication;
            this.iPerfilOpcionAplication = IPerfilOpcionAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }

        #region "Perfil"
        /*
        [HttpPost]
        [Route("RegisterPerfil")]
        public async Task<ActionResult> RegisterPerfil([FromBody] PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIPerfilAplication.RegisterPerfil(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }
        */

        [HttpPost]
        [Route("GetListPerfil")]
        public async Task<ActionResult> GetListPerfil([FromBody] PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iIPerfilAplication.GetListPerfil(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetListCbPerfil")]
        public async Task<ActionResult> GetListCbPerfil()
        {
            ResultDTO<ListCbDTO> res = new ResultDTO<ListCbDTO>();
            try
            {
                PerfilDTO request = new PerfilDTO();
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                request.vnombre_perfil = "";
                request.iid_estado_registro = -1;
                request.iid_perfil = -1;
                request.pageNum = 1;
                request.pageSize = 10000;
                ResultDTO<PerfilDTO> resper = await this.iIPerfilAplication.GetListPerfil(request);

                if (resper.IsSuccess) {
                    List<ListCbDTO> list = new List<ListCbDTO>();
                    foreach (PerfilDTO x in resper.data)
                    {
                        ListCbDTO item = new ListCbDTO();
                        item.id = x.iid_perfil;
                        item.value = x.vnombre_perfil;
                        item.value2 = x.vdescripcion_perfil;
                        list.Add(item);
                    }
                    res.data = list;
                    res.IsSuccess = resper.IsSuccess;
                    res.Message = resper.Message;
                    res.InnerException = resper.InnerException;
                }
              

                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        /*
        [HttpGet]
        [Route("GetPerfil")]
        public async Task<ActionResult> GetPerfil([FromQuery] int request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                PerfilDTO item = new PerfilDTO();
                item.iid_perfil = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iIPerfilAplication.GetPerfil(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }
        */

        [HttpDelete]
        [Route("DeletePerfil")]
        public async Task<ActionResult> DeletePerfil([FromQuery] int request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                PerfilDTO item = new PerfilDTO();
                item.iid_perfil = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iIPerfilAplication.DeletePerfil(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }



        #endregion



        #region "PerfilOpcion"




        [HttpGet]
        [Route("GetPerfilOpcion")]
        public async Task<ActionResult> GetPerfilOpcion([FromQuery] int request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
                PerfilOpcionDTO item = new PerfilOpcionDTO();
                item.iid_perfil_opcion = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iPerfilOpcionAplication.GetPerfilOpcion(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterPerfilOpcion")]
        public async Task<ActionResult> RegisterPerfilOpcion([FromBody] PerfilDTOResponse request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {

                // List<PerfilOpcionDTO> req = new List<PerfilOpcionDTO>();

                //request.perfil.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iPerfilOpcionAplication.RegisterPerfilOpcion(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListPerfilOpcion")]
        public async Task<ActionResult> GetListPerfilOpcion([FromBody] PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
                res = await this.iPerfilOpcionAplication.GetListPerfilOpcion(request);

                if (request.flg_accesos == true) 
                {
                    foreach (PerfilOpcionDTO x in res.data)
                    {
                        x.icrear = false;
                        x.iactualizar = false;
                        x.ieliminar = false;
                        x.ivisualizar = false;
                    }
                }
             

                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeletePerfilOpcion")]
        public async Task<ActionResult> DeletePerfilOpcion([FromQuery] int request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
                PerfilOpcionDTO item = new PerfilOpcionDTO();
                item.iid_perfil_opcion = request;
                
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iPerfilOpcionAplication.DeletePerfilOpcion(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        #endregion

        #region "Perfil Opcion Usuario"

        [HttpGet]
        [Route("GetPerfilOpcionUsuario")]
        public async Task<ActionResult> GetPerfilOpcionUsuario([FromQuery] int request)
        {
            //Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
            ResultDTO<PerfilUsuarioMenu> res = new ResultDTO<PerfilUsuarioMenu>();
            PerfilUsuarioMenu pfusuario = new PerfilUsuarioMenu();
            pfusuario.iid_usuario = request;
            try
            {
                res = await this.iPerfilOpcionAplication.GetPerfilOpcionUsuario(pfusuario);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetPerfilUsuario")]
        public async Task<ActionResult> GetPerfilUsuario([FromQuery] int request)
        {
            //Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
            //ResultDTO<PerfilUsuarioMenu> res_perfil =new ResultDTO<PerfilUsuarioMenu>();
            ResultDTO<List<Menu>> res = new ResultDTO<List<Menu>>();
            PerfilUsuarioMenu pfusuario = new PerfilUsuarioMenu();
            pfusuario.iid_usuario = request;
            try
            {
                List<Menu> lsmodulo = new List<Menu>();
                List<SubMenu> lsopcion = null;
                Menu modulo = null;
                ResultDTO<PerfilUsuarioMenu> res_perfil = await this.iPerfilOpcionAplication.GetPerfilOpcionUsuario(pfusuario);

                foreach (Menu x in res_perfil.item.menu) {
                    modulo = new Menu();
                    lsopcion = new List<SubMenu>();
                    modulo.iid_modulo = x.iid_modulo;
                    // modulo.iorden = x.iorden_modulo;
                    modulo.text = x.text;
                    modulo.link = x.link;
                    modulo.icon = x.icon;

                    modulo.iindica_visible_modulo = x.iindica_visible_modulo;
                    lsmodulo.Add(modulo);
                }
                res.item = lsmodulo;

                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetOpcionUsuario")]
        public async Task<ActionResult> GetOpcionUsuario([FromQuery] int request)
        {
            //Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
            //ResultDTO<PerfilUsuarioMenu> res = new ResultDTO<PerfilUsuarioMenu>();
            ResultDTO<List<SubMenu>> res = new ResultDTO<List<SubMenu>>();
            PerfilUsuarioMenu pfusuario = new PerfilUsuarioMenu();
            pfusuario.iid_usuario = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); ;
            try
            {
               // List<Menu> lsmodulo = new List<Menu>();
                List<SubMenu> lsopcion = null;
            //    Menu modulo = null;
                ResultDTO<PerfilUsuarioMenu> res_perfil = await this.iPerfilOpcionAplication.GetPerfilOpcionUsuario(pfusuario);

                if (res_perfil.IsSuccess) {

                    var menu = res_perfil.item.menu.Find(xx => xx.iid_modulo == request);

                    foreach (SubMenu x in menu.submenu)
                    {
                        SubMenu opcion = new SubMenu();
                        opcion.iid_modulo = x.iid_modulo;
                        opcion.iid_opcion = x.iid_opcion;
                        //opcion.iindica_visible = x.iid;
                        //opcion.iorden = y.iorden_opcion;
                        opcion.text = x.text;
                        opcion.icon = x.icon;
                        opcion.iindica_visible_opcion = x.iindica_visible_opcion;
                        opcion.link = x.link;

                        opcion.icrear = x.icrear;
                        opcion.ivisualizar = x.ivisualizar;
                        opcion.ieliminar = x.ieliminar;
                        opcion.iactualizar = x.iactualizar;

                        lsopcion.Add(opcion);
                        // modulo.submenu = lsopcion;
                    }
                     res.item = lsopcion;

                }

                

                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);
                return BadRequest(res);
            }
        }
        #endregion
         

    }
}