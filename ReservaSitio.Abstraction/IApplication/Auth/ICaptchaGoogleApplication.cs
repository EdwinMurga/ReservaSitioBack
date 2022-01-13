using System;

namespace ReservaSitio.Abstraction.IApplication.Auth
{
    public interface ICaptchaGoogleApplication
    {
        bool ValidateCaptcha(string token);
    }
}