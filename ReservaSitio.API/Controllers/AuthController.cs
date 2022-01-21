using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.API.Configuration;
using ReservaSitio.API.DTOs;
using ReservaSitio.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction;
using ReservaSitio.Application.Autenticacion;
using System.Net.Http.Json;
using Newtonsoft.Json;
using ReservaSitio.Abstraction.IApplication.Auth;

using ReservaSitio.Abstraction.IService.Auth;
using ReservaSitio.DTOs.Auth;

using RestSharp;
using ReservaSitio.Application;
using Microsoft.Extensions.Logging;
using ReservaSitio.DTOs;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.Abstraction.IApplication.Usuario;

namespace ReservaSitio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ITokenHandlerService _service;
        private IAuthenticationService _authService;
        private readonly IConfiguration Configuration;
        private IAutenticacion _autenticacion;
        private IAuthenticationApplication _authApplication;
        private static HttpClient client = new HttpClient();
        private AuthenticationResponse existingUser = new AuthenticationResponse();
     
        private HttpClient _httpClient = new HttpClient();

        public int intEstadoBloqueado = 4;
       private ILogger<AuthController> _logger;

        private readonly ILogErrorAplication iLogErrorAplication;
        private readonly IUsuarioAplication iIUsuarioAplication;
        public AuthController(
            UserManager<IdentityUser> userManager, 
            ITokenHandlerService service,
            IAutenticacion autenticacion, 
            IAuthenticationApplication authApplication,
            IAuthenticationService authService, 
            IConfiguration configuration, 
            ILogger<AuthController> logger,

            ILogErrorAplication ILogErrorAplication,
            IUsuarioAplication IUsuarioAplication
            )
        {
            this.Configuration = configuration;
            _userManager = userManager;
            _autenticacion = autenticacion;
         _service = service;
            _authApplication = authApplication;
            _authService = authService;
            _logger = logger;

            this.iLogErrorAplication = ILogErrorAplication;
            this.iIUsuarioAplication = IUsuarioAplication;
        }

 
        //[HttpPost]
        //[Route("GenerateTokenLogin")]
       /* public IActionResult GenerateJwtTokenLogin([FromBody] ITokenParameters pars)
        {
            try
            {
                string jwtToken = _service.GenerateToken(pars);
                _logger.LogInformation("This is an INFORMATION message.");
                return Ok(jwtToken);
            }
            catch (Exception e)
            {
                _logger.LogError("This is an ERROR message.");
                return BadRequest(e.Message);
            }
        }*/

        //[HttpPost]
        //[Route("DesencriptTokenLogin/{token}")]
        /*public IActionResult GetObjectToken(string token)
        {
            try
            {
                ITokenParameters pars = new ITokenParameters();
                pars = _service.GetObjectToken(token);
                return Ok(pars);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        //[HttpPost]
        //[Route("GenerateTokenPasswordRecover")]
       /* public IActionResult GenerateJwtTokenPasswordRecover([FromBody] ITokenParameters pars)
        {
            try
            {
                string jwtToken = _service.GenerateJwtTokenPasswordRecover(pars);
                return Ok(jwtToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        //[HttpPost]
       // [Route("DesencriptTokenPassword/{token}")]
        /*public IActionResult GetObjectTokenPasswordRecover(string token)
        {
            try
            {
                ITokenParameters pars = new ITokenParameters();
                pars = _service.GetObjectTokenPasswordRecover(token);
                return Ok(pars);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO resquest) 
        {

            LoginResponseDTO<string> resLogin = new LoginResponseDTO<string>();
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            UsuarioDTO resUser = new UsuarioDTO();
            resUser.vcorreo_electronico = resquest.usuario;
            resUser.vclave = resquest.clave;

            try
            {
                res = await this.iIUsuarioAplication.GetUsuarioParameter(resUser);
                if (res.IsSuccess && res.item != null)
                {


                    resLogin.IsSuccess = true;
                    resLogin.Message = "Acceso Correcto";
                    resLogin.Token = "";

                }
                else {
                    resLogin.Informacion = " Usuario no Encontrado";
                }



                return Ok(resLogin);
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
