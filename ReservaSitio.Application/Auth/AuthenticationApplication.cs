using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IApplication.Auth;
using ReservaSitio.Abstraction.IService.Auth;
using ReservaSitio.API.DTOs;
using ReservaSitio.Services;

namespace ReservaSitio.Application.Auth
{
    public class AuthenticationApplication : IAuthenticationApplication
    {
        private readonly ICaptchaGoogleApplication _captchaGoogleApp;
       
        
        public AuthenticationApplication(   ICaptchaGoogleApplication captchaGoogleApp)
        {        
            _captchaGoogleApp = captchaGoogleApp;
        }
  
        public  Boolean validarGoogleCaptcha(LoginDTO user)
        {
            return _captchaGoogleApp.ValidateCaptcha(user.GoogleToken);           
        }

    }
}
