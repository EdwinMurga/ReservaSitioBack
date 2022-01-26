using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Perfiles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly IPerfilAplication iIPerfilAplication;
        private readonly IPerfilOpcionAplication iPerfilOpcionAplication;


        public PerfilController(IPerfilAplication _iIPerfilAplication
            , IPerfilOpcionAplication IPerfilOpcionAplication)
        {
            this.iIPerfilAplication = _iIPerfilAplication;
            this.iPerfilOpcionAplication = IPerfilOpcionAplication;
        }

        #region "Perfil"
        [HttpPost]
        [Route("RegisterPerfil")]
        public async Task<ActionResult> RegisterPerfil([FromBody] PerfilDTO request)
        {
            ResultDTO<PerfilDTO> res = new ResultDTO<PerfilDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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
#endregion



        #region "PerfilOpcion"

        [HttpGet]
        [Route("GetPerfilOpcion")]
        public async Task<ActionResult> GetPerfilOpcion([FromQuery] int request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
                PerfilOpcionDTO item = new PerfilOpcionDTO();
                item.iid_perfil_opcion = request;
                res = await this.iPerfilOpcionAplication.GetPerfilOpcion(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterPerfilOpcion")]
        public async Task<ActionResult> RegisterPerfilOpcion([FromBody] List<PerfilOpcionDTO> request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
              //  request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iPerfilOpcionAplication.RegisterPerfilOpcion(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListPerfilOpcion")]
        public async Task<ActionResult> GetListPerfilOpcion([FromBody] PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            try
            {
                res = await this.iPerfilOpcionAplication.GetListPerfilOpcion(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();
                return BadRequest(res);
            }
        }

        #endregion

        #region "Perfil Opcion "

        [HttpPost]
        [Route("GetPerfilOpcionUsuario")]
        public async Task<ActionResult> GetPerfilOpcionUsuario([FromQuery] int request)
        {
            ResultDTO<PerfilUsuarioDTO> res = new ResultDTO<PerfilUsuarioDTO>();
            PerfilUsuarioDTO pfusuario = new PerfilUsuarioDTO();
            pfusuario.iid_usuario = request;
            try
            {
                res = await this.iPerfilOpcionAplication.GetPerfilOpcionUsuario(pfusuario);
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
