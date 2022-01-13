using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Opciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : Controller
    {
        // GET: ModuloController


        private readonly IModuloAplication iModuloAplication;

        public ModuloController(IModuloAplication _iIPerfilAplication)
        {
            this.iModuloAplication = _iIPerfilAplication;
        }


        [HttpPost]
        [Route("RegisterModulo")]
        public async Task<ActionResult> RegisterModulo([FromBody] ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            try
            {
                res = await this.iModuloAplication.RegisterModulo(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListModulo")]
        public async Task<ActionResult> GetListModulo([FromBody] ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            try
            {
                res = await this.iModuloAplication.GetListModulo(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetModulo")]
        public async Task<ActionResult> GetModulo([FromQuery] int request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            try
            {
                ModuloDTO item = new ModuloDTO();
                item.iid_modulo = request;
                res = await this.iModuloAplication.GetModulo(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }

        }
    }
}
