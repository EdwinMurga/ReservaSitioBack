using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.Abstraction.IService.Auth;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservaSitio.Entities.GoogleCaptcha;

namespace ReservaSitio.Services.Auth
{
    public class CaptchaGoogleService : ICaptchaGoogleService
    {
        private readonly string _key;
        private readonly string _urlGoogle;

        public CaptchaGoogleService(IConfiguration config)
        {
            _key = config["SecretKeyCaptcha"];
            _urlGoogle = config["UrlGoogleCaptcha"];
        }
        public bool ValidateCaptcha(string token)
        {
            string encodedResponse = token;
            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var secret = _key;
            var url = _urlGoogle;
            if (string.IsNullOrEmpty(secret)) return false;

            var client = new System.Net.WebClient();

            var googleReply = client.DownloadString(
                $"{url}?secret={secret}&response={encodedResponse}");

            return JsonConvert.DeserializeObject<RecaptchaResponse>(googleReply).Success;
        }
    }
}
