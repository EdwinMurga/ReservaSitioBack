using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.API.DTOs;

namespace ReservaSitio.Abstraction.IApplication.Auth
{
    public interface IAuthenticationApplication
    {
       
        Boolean validarGoogleCaptcha(LoginDTO user);
       // bool registrarIntentoBloqueo(string strNumeroDocumento, int op);
    }
}
