using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IApplication.Auth;
using ReservaSitio.Abstraction.IService.Auth;
using ReservaSitio.DTOs.Auth;
using ReservaSitio.Services;

namespace ReservaSitio.Application.Auth
{
    public class AuthenticationApplication : IAuthenticationApplication
    {
        private IAuthenticationService _authService;
       // private IUsuarioService _usuarioService;
        private ICaptchaGoogleApplication _captchaGoogleApp;
       
        
        public AuthenticationApplication(
            IAuthenticationService authService, 
          //  IUsuarioService usuarioService,
            ICaptchaGoogleApplication captchaGoogleApp)
        {
            _authService = authService;
           // _usuarioService = usuarioService;
            _captchaGoogleApp = captchaGoogleApp;
        }
  
        public  Boolean validarGoogleCaptcha(UserLoginRequestDTO user)
        {
            bool validate =  _captchaGoogleApp.ValidateCaptcha(user.GoogleToken);
            return validate;
        }

       /* public bool registrarIntentoBloqueo(string strNumeroDocumento, int op)
        {
            return _usuarioService.ActualizarIntentoBloqueo(strNumeroDocumento, op);
        }*/
    }
}
