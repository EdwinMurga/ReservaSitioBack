using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.Application.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.ParametroAplicacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroAplicacionController : Controller
    {

        private readonly IParametroAplication iParametroAplication;
        private readonly ILogErrorAplication iLogErrorAplication;

        public ParametroAplicacionController(IParametroAplication _iIPerfilAplication,
            ILogErrorAplication ILogErrorAplication)
        {
            this.iParametroAplication = _iIPerfilAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }


        [HttpPost]
        [Route("RegisterParametro")]
        public async Task<ActionResult> RegisterParametro([FromBody] ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                res = await this.iParametroAplication.RegisterParametro(request);
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
        [Route("GetListParametro")]
        public async Task<ActionResult> GetListParametro([FromBody] ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                res = await this.iParametroAplication.GetListParametro(request);
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
        [Route("GetParametro")]
        public async Task<ActionResult> GetParametro([FromQuery] int request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                ParametroAplicacionDTO item = new ParametroAplicacionDTO();
                item.iid_parametro = request;
                res = await this.iParametroAplication.GetParametro(item);
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
