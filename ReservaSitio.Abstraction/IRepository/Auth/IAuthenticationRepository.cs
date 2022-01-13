using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Auth;

namespace ReservaSitio.Abstraction.IRepository.Auth
{
    public interface IAuthenticationRepository
    {
        
        bool CheckPasswordAsync(string Password, string userPassword);
        //Task<ResultDTO<AuthenticationResponse>> InsertLogLogeo(AuthenticationResponse request);
    }
}
