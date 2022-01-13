using System;

namespace ReservaSitio.Abstraction.IService.Auth
{
    public interface ICaptchaGoogleService
    {
        bool ValidateCaptcha(string token);
    }
}