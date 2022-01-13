using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs.Auth;

namespace ReservaSitio.Abstraction.IApplication.Auth
{
    public interface IAuthenticationApplication
    {
       
        Boolean validarGoogleCaptcha(UserLoginRequestDTO user);
       // bool registrarIntentoBloqueo(string strNumeroDocumento, int op);
    }
}
