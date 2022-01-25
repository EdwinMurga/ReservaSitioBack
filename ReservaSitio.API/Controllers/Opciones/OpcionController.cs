using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class OpcionController : Controller
    {
        // GET: OpcionController

        private readonly IOpcionAplication iOpcionAplication;
      
        public OpcionController(IOpcionAplication IOpcionAplication
            , IPerfilOpcionAplication IPerfilOpcionAplication)
        {
            this.iOpcionAplication = IOpcionAplication;
        
        }

        #region "Opcion"


        [HttpPost]
        [Route("RegisterOpcion")]
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
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListOpcion")]
        public async Task<ActionResult> GetListOpcion([FromBody] OpcionDTO request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            try
            {
                res = await this.iOpcionAplication.GetListOpcion(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetOpcion")]
        public async Task<ActionResult> GetOpcion([FromQuery] int request)
        {
            ResultDTO<OpcionDTO> res = new ResultDTO<OpcionDTO>();
            try
            {
                OpcionDTO item = new OpcionDTO();
                item.iid_opcion = request;
                res = await this.iOpcionAplication.GetOpcion(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }


        #endregion


       
    }
}
