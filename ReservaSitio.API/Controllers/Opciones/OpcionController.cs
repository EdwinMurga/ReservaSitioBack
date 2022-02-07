using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Opciones
{
     [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OpcionController : Controller
    {
        // GET: OpcionController

        private readonly IOpcionAplication iOpcionAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        public OpcionController(IOpcionAplication IOpcionAplication
            , IPerfilOpcionAplication IPerfilOpcionAplication
               , ILogErrorAplication ILogErrorAplication)
        {
            this.iOpcionAplication = IOpcionAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }

        #region "Opcion"


        //[HttpPost]
        //[Route("RegisterOpcion")]
        /*
        public async Task<ActionResult> RegisterOpcion([FromBody] OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iOpcionAplication.RegisterOpcion(request);
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
/*
       [HttpPost]
        [Route("GetListOpcion")]
        public async Task<ActionResult> GetListOpcion([FromBody] OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iOpcionAplication.GetListOpcion(request);
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
        [Route("GetListOpcionByModulo")]
        public async Task<ActionResult> GetListOpcionByModulo([FromBody] ModuloDTO request) 
        {
            ResultDTO<OpcionPerfilDTO> res = new ResultDTO<OpcionPerfilDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iOpcionAplication.GetListOpcionByModulo(request);
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

        //[HttpGet]
        //[Route("GetOpcion")]
        /*
        public async Task<ActionResult> GetOpcion([FromQuery] int request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            try
            {
                OpcionDTO item = new OpcionDTO();
                item.iid_opcion = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iOpcionAplication.GetOpcion(item);
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
        //[HttpDelete]
        //[Route("DeleteOpcion")]
        /*
    public async Task<ActionResult> DeleteOpcion([FromQuery] int request)
    {
        ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
        try
        {
            OpcionDTO item = new OpcionDTO();
            item.iid_opcion = request;
            item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            res = await this.iOpcionAplication.DeleteOpcion(item);
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
        #endregion



    }
}
