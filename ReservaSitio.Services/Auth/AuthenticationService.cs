using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using ReservaSitio.Abstraction.IRepository.Auth;
using ReservaSitio.Abstraction.IService.Auth;
using ReservaSitio.DTOs.Auth;
using ReservaSitio.Abstraction;


namespace ReservaSitio.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
 
        private IConfiguration Configuration;
        private IAuthenticationRepository _authRepository;
        private HttpClient _httpClient = new HttpClient();

        public AuthenticationService(IAuthenticationRepository authRepository, IConfiguration configuration )
        {
            _authRepository = authRepository;
            Configuration = configuration;

        }
    
    
    }
}
