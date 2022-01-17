using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Local
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : Controller
    {
        private readonly ILocalAplication iLocalAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        public  LocalController (ILocalAplication ILocalAplication
           , ILogErrorAplication ILogErrorAplication)
        {
            this.iLocalAplication = ILocalAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }

        [HttpPost]
        [Route("RegisterLocal")]
        public async Task<ActionResult> RegisterLocal([FromBody] LocalDTO request)
        {
            ResultDTO<LocalDTO> res = new ResultDTO<LocalDTO>();
            try
            {
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
                this.iLogErrorAplication.RegisterLogError(lg);

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
                this.iLogErrorAplication.RegisterLogError(lg);

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
                this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }


    }
}
