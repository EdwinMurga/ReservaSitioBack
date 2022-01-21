using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.DTOs
{
    public class LoginDTO
    {
        public string usuario { set; get; }
        public string clave { set; get; }
        public string tokenRecaptcha { set; get; }
    }


    public class LoginResponseDTO<T>
    {
        public Boolean IsSuccess { get; set; }
        public string Message { get; set; }
        public string MessageExeption { get; set; }
        public string Informacion { get; set; }
        public List<T> data { get; set; }
        public string Token{get;set;}
    }

}
