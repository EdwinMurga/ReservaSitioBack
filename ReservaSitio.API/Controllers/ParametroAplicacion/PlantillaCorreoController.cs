using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.ParametroAplicacion
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class PlantillaCorreoController : Controller
    {
        // GET: PlantillaCorreoController
        private readonly IPlantillaCorreoAplication iPlantillaCorreoAplication;
        private readonly ILogErrorAplication iLogErrorAplication;

        public PlantillaCorreoController(IPlantillaCorreoAplication IPlantillaCorreoAplication,
            ILogErrorAplication ILogErrorAplication)
        {
            this.iPlantillaCorreoAplication = IPlantillaCorreoAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }


        [HttpPost]
        [Route("RegisterPlantillaCorreo")]
        public async Task<ActionResult> RegisterPlantillaCorreo([FromBody] PlantillaCorreoDTO request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iPlantillaCorreoAplication.RegisterPlantillaCorreo(request);
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
        [Route("GetListPlantillaCorreo")]
        public async Task<ActionResult> GetListPlantillaCorreo([FromBody] PlantillaCorreoDTO request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iPlantillaCorreoAplication.GetListPlantillaCorreo(request);
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
        [Route("GetPlantillaCorreo")]
        public async Task<ActionResult> GetPlantillaCorreo([FromQuery] int request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            try
            {
                PlantillaCorreoDTO item = new PlantillaCorreoDTO();
                item.iid_plantilla_correo = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iPlantillaCorreoAplication.GetPlantillaCorreo(item);
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
       // [Route("DeletePlantillaCorreo")]
        public async Task<ActionResult> DeletePlantillaCorreo([FromQuery] int request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            try
            {
                PlantillaCorreoDTO item = new PlantillaCorreoDTO();
                item.iid_plantilla_correo = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iPlantillaCorreoAplication.DeletePlantillaCorreo(item);
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
