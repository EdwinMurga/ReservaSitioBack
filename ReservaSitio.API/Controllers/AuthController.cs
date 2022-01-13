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
        public ResponseDTO _ResponseDTO;
        private HttpClient _httpClient = new HttpClient();

        public int intEstadoBloqueado = 4;
       private ILogger<AuthController> _logger;
        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service,
            IAutenticacion autenticacion, IAuthenticationApplication authApplication,
            IAuthenticationService authService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            this.Configuration = configuration;
            _userManager = userManager;
            _autenticacion = autenticacion;
         _service = service;
            _authApplication = authApplication;
            _authService = authService;
            _logger = logger;
        }

 
        [HttpPost]
        [Route("GenerateTokenLogin")]
        public IActionResult GenerateJwtTokenLogin([FromBody] ITokenParameters pars)
        {
            try
            {
                string jwtToken = _service.GenerateJwtTokenLogin(pars);
                _logger.LogInformation("This is an INFORMATION message.");
                return Ok(jwtToken);
            }
            catch (Exception e)
            {
                _logger.LogError("This is an ERROR message.");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("DesencriptTokenLogin/{token}")]
        public IActionResult GetObjectToken(string token)
        {
            try
            {
                ITokenParameters pars = new ITokenParameters();
                pars = _service.GetObjectTokenLogin(token);
                return Ok(pars);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("GenerateTokenPasswordRecover")]
        public IActionResult GenerateJwtTokenPasswordRecover([FromBody] ITokenParameters pars)
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
        }

        [HttpPost]
        [Route("DesencriptTokenPassword/{token}")]
        public IActionResult GetObjectTokenPasswordRecover(string token)
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
        }

  

 
    }
}
