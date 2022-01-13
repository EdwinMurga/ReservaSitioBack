using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Usuario;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAplication iUsuarioAplication;

        public UsuarioController(IUsuarioAplication IUsuarioAplication)
        {
            this.iUsuarioAplication = IUsuarioAplication;
        }


        [HttpPost]
        [Route("RegisterUsuario")]
        public async Task<ActionResult> RegisterUsuario([FromBody] UsuarioDTO request)
        {
            ResultDTO<UsuarioDTO> res= new ResultDTO<UsuarioDTO>();
            try
            {
                res = await this.iUsuarioAplication.RegisterUsuario(request);
                return Ok(res);
            }
            catch(Exception e)
            {
                res.InnerException = e.Message.ToString();
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
                res = await this.iUsuarioAplication.GetListUsuario(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
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
                res = await this.iUsuarioAplication.GetUsuario(item);
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
