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
    //[Route("api/[controller]")]
    //[ApiController]
  //  [Authorize]
    public class ModuloController : Controller
    {
        // GET: ModuloController
        private readonly IModuloAplication iModuloAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        public ModuloController(IModuloAplication _iIPerfilAplication
            , ILogErrorAplication ILogErrorAplication)
        {
            this.iModuloAplication = _iIPerfilAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }



        //[HttpPost]
        //[Route("GetListModulo")]
        public async Task<ActionResult> GetListModulo([FromBody] ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iModuloAplication.GetListModulo(request);
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


        //[HttpDelete]
        //[Route("DeleteModulo")]
        /* public async Task<ActionResult> DeleteModulo([FromQuery] int request)
         {
             ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
             try
             {
                 ModuloDTO item = new ModuloDTO();
                 item.iid_modulo = request;
                 item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                 res = await this.iModuloAplication.DeleteModulo(item);
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
         }*/

        //[HttpPost]
        //[Route("RegisterModulo")]
        /*public async Task<ActionResult> RegisterModulo([FromBody] ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iModuloAplication.RegisterModulo(request);
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
        }*/

        //[HttpPost]
        //[Route("GetListModulo")]
        /* public async Task<ActionResult> GetListModulo([FromBody] ModuloDTO request)
         {
             ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
             try
             {
                 request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                 res = await this.iModuloAplication.GetListModulo(request);
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
         }*/

        //[HttpGet]
        //[Route("GetModulo")]
        /*  public async Task<ActionResult> GetModulo([FromQuery] int request)
          {
              ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
              try
              {
                  ModuloDTO item = new ModuloDTO();
                  item.iid_modulo = request;
                  item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                  res = await this.iModuloAplication.GetModulo(item);
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

          }*/
    }
}
