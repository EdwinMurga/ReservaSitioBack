using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Local
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocalController : Controller
    {
        private readonly ILocalAplication iLocalAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        private readonly IPisoAplication iIPisoAplication;
        public  LocalController (ILocalAplication ILocalAplication
           , ILogErrorAplication ILogErrorAplication
            , IPisoAplication IPisoAplication)
        {
            this.iLocalAplication = ILocalAplication;
            this.iLogErrorAplication = ILogErrorAplication;
            this.iIPisoAplication = IPisoAplication;
        }

        #region "Local"

        [HttpPost]
        [Route("RegisterLocal")]
        public async Task<ActionResult> RegisterLocal([FromBody] LocalDTO request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iLocalAplication.RegisterLocal(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListLocal")]
        public async Task<ActionResult> GetListLocal([FromBody] LocalDTO request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            try
            {
                res = await this.iLocalAplication.GetListLocal(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetLocal")]
        public async Task<ActionResult> GetLocal([FromQuery] int request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            try
            {
                LocalDTO item = new LocalDTO();
                item.iid_local = request;
                res = await this.iLocalAplication.GetLocal(item);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await  this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        #endregion


        #region "Piso"



        [HttpPost]
        [Route("RegisterPiso")]
        public async Task<ActionResult> RegisterPiso([FromBody] PisoDTO request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIPisoAplication.RegisterPiso(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListPiso")]
        public async Task<ActionResult> GetListPiso([FromBody] PisoDTO request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            try
            {
                res = await this.iIPisoAplication.GetListPiso(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetPiso")]
        public async Task<ActionResult> GetPiso([FromQuery] int request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            try
            {
                PisoDTO item = new PisoDTO();
                item.iid_piso = request;
                res = await this.iIPisoAplication.GetPiso(item);
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
                lg.iid_usuario_registra = 0;
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
