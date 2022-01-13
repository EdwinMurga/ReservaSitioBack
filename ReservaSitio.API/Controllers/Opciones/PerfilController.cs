using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Perfiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller
    {
        private readonly IPerfilAplication iIPerfilAplication;

        public PerfilController(IPerfilAplication _iIPerfilAplication)
        {
            this.iIPerfilAplication = _iIPerfilAplication;
        }


        [HttpPost]
        [Route("RegisterPerfil")]
        public async Task<ActionResult> RegisterPerfil([FromBody] PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                res = await this.iIPerfilAplication.RegisterPerfil(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
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
                res = await this.iIPerfilAplication.GetListPerfil(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
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
                res = await this.iIPerfilAplication.GetPerfil(item);
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
