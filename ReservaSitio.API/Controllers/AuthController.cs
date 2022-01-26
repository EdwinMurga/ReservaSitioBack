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




using RestSharp;
using ReservaSitio.Application;
using Microsoft.Extensions.Logging;

using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.Abstraction.IApplication.Usuario;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Auth;
using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.Abstraction.IApplication.Empresa;
using System.Security.Claims;
using ReservaSitio.Abstraction.IApplication.Util;

namespace ReservaSitio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService iITokenHandlerService;     

        private readonly IConfiguration Configuration;
        private readonly IAutenticacion iIAutenticacion;
        private readonly IAuthenticationApplication iIAuthenticationApplication;
        private readonly ILogger<AuthController> _logger;

        private readonly ILogErrorAplication iLogErrorAplication;
        private readonly IUsuarioAplication iIUsuarioAplication;
        private readonly IPerfilAplication iIPerfilAplication;
        private readonly IEmpresaAplication iIEmpresaAplicacion;

        private readonly IUtilAplication iIUtilAplication;

        //   private static HttpClient client = new HttpClient();
        //  private AuthenticationResponse existingUser = new AuthenticationResponse();
        //private IAuthenticationService _authService;
        // private IAutenticacion _autenticacion;
        //private HttpClient _httpClient = new HttpClient();
        public AuthController(
       
            ITokenHandlerService ITokenHandlerService,
            IConfiguration configuration,
            IAutenticacion autenticacion, 
            IAuthenticationApplication IAuthenticationApplication,     
            
            ILogger<AuthController> logger,
            ILogErrorAplication ILogErrorAplication,

            IUsuarioAplication IUsuarioAplication,
            IPerfilAplication IPerfilAplication,
            IEmpresaAplication IEmpresaAplication,

            IUtilAplication IUtilAplication
            // UserManager<IdentityUser> userManager, 
            // IAuthenticationService authService, 
            )
        {
            this.Configuration = configuration;          
            this.iITokenHandlerService = ITokenHandlerService;
            this.iIAuthenticationApplication = IAuthenticationApplication;
            this._logger = logger;

            this.iLogErrorAplication = ILogErrorAplication;
            this.iIUsuarioAplication = IUsuarioAplication;
            this.iIPerfilAplication = IPerfilAplication;
            this.iIEmpresaAplicacion = IEmpresaAplication;

            this.iIUtilAplication = IUtilAplication;
            // _userManager = userManager;
            // _autenticacion = autenticacion;
            // _authService = authService;
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

            LoginResponseDTO<AuthenticationResponse> resLogin = new LoginResponseDTO<AuthenticationResponse>();
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            UsuarioDTO resUser = new UsuarioDTO();
            resUser.vcorreo_electronico = resquest.UserName;
            resUser.vclave = resquest.Password;
            string contrasena = String.Empty;

            try
            {
                var validCaptcha = iIAuthenticationApplication.validarGoogleCaptcha(resquest);
                res = await this.iIUsuarioAplication.GetUsuarioParameter(resUser);

                /*if (!validCaptcha)
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Error : token Recatcha!";
                    return Ok(resLogin);
                }               
                else*/ if (!res.IsSuccess && res.item == null)
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Usuario No Registrado";
                    resLogin.Token = "";                  

                    return Ok(resLogin);
                }
                else if  (res.IsSuccess && res.item != null  && resquest.Password != res.item.vclave  && res.item.iid_indica_bloqueo == 0) // && res.item.cantidad_intentos >=3
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Usuario, "+ (res.item.cantidad_intentos >= 3 ? " bloqueado excedio los intentos de logeo " : " intento de logeo  " + (res.item.cantidad_intentos==0? "": res.item.cantidad_intentos.ToString()))  ;
                    resLogin.Token = "";

                    /** registra intento de logeo **/
                    resUser.iid_usuario = res.item.iid_usuario;
                    await this.iIUsuarioAplication.RegisterUsuarioIntentoLogeo(resUser);

                    return Ok(resLogin);
                }

                else if (res.IsSuccess && res.item != null && res.item.iid_indica_bloqueo == 1 )  
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Usuario, "+ res.item.vcorreo_electronico + " actualmente bloqueado";
                    resLogin.Token = "";

                    return Ok(resLogin);
                }

                else
                {

                    resLogin.IsSuccess = true;
                    resLogin.Message = "Usuario, Logeado ";


                    /********registra acceso****/
                    res.item.iid_usuario_registra = res.item.iid_usuario;

                    await this.iIUsuarioAplication.RegisterUsuarioAcceso(res.item);
                    // await this.iIUsuarioAplication.RegisterUsuario(resUser);
                    /*********info usuario ************/

                    var resperf = await this.iIPerfilAplication.GetPerfil(new ReservaSitio.DTOs.Opciones.PerfilDTO { iid_perfil = res.item.iid_perfil });
                    var resempr = await this.iIEmpresaAplicacion.GetEmpresa(new ReservaSitio.DTOs.Empresa.EmpresaDTO { iid_empresa = res.item.iid_empresa });

                
                    AuthenticationResponse usrRespon = new AuthenticationResponse();
                    usrRespon.iid_usuario = res.item.iid_usuario;
                    usrRespon.vnombres = res.item.vnombres;
                    usrRespon.vapellido_materno = res.item.vapellido_materno;
                    usrRespon.vapellido_paterno = res.item.vapellido_paterno;
                    usrRespon.vcorreo_electronico = res.item.vcorreo_electronico;
                    usrRespon.iid_perfil = res.item.iid_perfil;
                    usrRespon.perfil = resperf.item.vnombre_perfil;
                    usrRespon.iid_tipo_documento = res.item.iid_tipo_documento;
                    usrRespon.vnumero_telefonico = res.item.vnumero_telefonico;
                    usrRespon.vnombres = res.item.vnombres;
                    usrRespon.vnro_documento = res.item.vnro_documento;
                    usrRespon.dfecha_caduca_clave = res.item.dfecha_caduca_clave;
                    usrRespon.dfecha_ultimo_acceso = res.item.dfecha_ultimo_acceso;
                    usrRespon.iid_empresa = res.item.iid_empresa;
                    usrRespon.empresa = resempr.item.vnombre_completo;


                    resLogin.data = usrRespon;
                    /********* info usuario ************/

                    /********* token usuario ************/
                    ITokenParameters tparm = new ITokenParameters();
                    tparm.UserName = res.item.vcorreo_electronico;
                    tparm.PasswordHash = res.item.vclave;
                    tparm.Id = res.item.iid_usuario.ToString();
                    tparm.FechaCaduca = DateTime.Now;// res.item.dfecha_caduca_clave;

                    resLogin.Token =   this.iITokenHandlerService.GenerateToken(tparm);

                    return Ok(resLogin);
                }    

               
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
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }

        }

        [HttpPost]
        [Route("RecuperaContrasena")]
        public async Task<IActionResult> RecuperaContrasena([FromBody] LoginRecuperacionDTO request) 
        {
            LoginResponseDTO<string> resLogin = new LoginResponseDTO<string>();
            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            try 
            {
               
                UsuarioDTO resUser = new UsuarioDTO();
                LoginDTO lgdto = new LoginDTO();
                lgdto.GoogleToken = request.GoogleToken;
                resUser.vcorreo_electronico = request.UserName;
                //resUser.vclave = request.Password;
                var validCaptcha = iIAuthenticationApplication.validarGoogleCaptcha(lgdto);
                res = await this.iIUsuarioAplication.GetUsuarioParameter(resUser);
                /*if (!validCaptcha)
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Error : token Recatcha!";
                    return Ok(resLogin);
                }               
                else*/  if (!res.IsSuccess && res.item == null)
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Usuario No Registrado";
                    resLogin.Token = "";
                    return Ok(resLogin);
                }
                else {

                    resLogin.IsSuccess = true;
                    resLogin.Message = "Usuario, su clave fue recuperada ";
                    resLogin.Token = "";

                    /***********generar token o coigo de reset contraseña**********/


                    /***********enviar mail al usuario**********/
                    int paramPlantilla = 1;
                    ResultDTO<bool> resmail = await  this.iIUtilAplication.envioMailPlantilla(paramPlantilla, new int[res.item.iid_usuario]);

                    resLogin.IsSuccess = resmail.IsSuccess;
                    resLogin.Message = resmail.Message;
                    resLogin.MessageExeption = resmail.MessageExeption;

                    return Ok(resLogin);

                }

              
            }
            catch(Exception e) 
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
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }

        }



    }
}
