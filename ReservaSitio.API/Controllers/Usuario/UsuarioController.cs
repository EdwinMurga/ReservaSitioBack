using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.Usuario;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Usuario
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAplication iUsuarioAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        public UsuarioController(IUsuarioAplication IUsuarioAplication
            , ILogErrorAplication ILogErrorAplication)
        {
            this.iUsuarioAplication = IUsuarioAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }


        [HttpPost]
        [Route("RegisterUsuario")]
        public async Task<ActionResult> RegisterUsuario([FromBody] UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res= new ResultDTO<UsuarioDTO>();
            try
            {
               
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iUsuarioAplication.RegisterUsuario(request);
                return Ok(res);
            }
            catch(Exception e)
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
                await  this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListUsuario")]
        public async Task<ActionResult> GetListUsuario([FromBody] UsuarioListDTO request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iUsuarioAplication.GetListUsuario(request);
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
        [Route("GetUsuario")]
        public async Task<ActionResult> GetUsuario([FromQuery] int request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            try
            {
                UsuarioDTO item = new UsuarioDTO();
                item.iid_usuario = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iUsuarioAplication.GetUsuario(item);
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
        //[Route("EliminarUsuario")]
        public async Task<ActionResult> EliminarUsuario([FromQuery] int request)
        {
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            try
            {
                UsuarioDTO item = new UsuarioDTO();
                item.iid_usuario = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iUsuarioAplication.DeleteUsuario(item);
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

    }
}
