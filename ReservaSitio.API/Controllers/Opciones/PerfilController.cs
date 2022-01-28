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
    [Authorize]
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

        [HttpDelete]
        //[Route("DeletePerfil")]
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
        public async Task<ActionResult> RegisterPerfilOpcion([FromBody] List<PerfilOpcionDTO> request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
              //  request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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

        #region "Perfil Opcion "

        [HttpPost]
        [Route("GetPerfilOpcionUsuario")]
        public async Task<ActionResult> GetPerfilOpcionUsuario([FromQuery] int request)
        {
            ResultDTO<PerfilUsuarioDTO> res = new ResultDTO<PerfilUsuarioDTO>();
            PerfilUsuarioDTO pfusuario = new PerfilUsuarioDTO();
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

        #endregion

    }
}
