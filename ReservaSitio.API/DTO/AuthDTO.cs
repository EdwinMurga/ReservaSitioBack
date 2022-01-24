using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.DTOs
{
  

    public class LoginResponseDTO<T>
    {
        public Boolean IsSuccess { get; set; }
        public string Message { get; set; }
        public string MessageExeption { get; set; }
        public string Informacion { get; set; }
        public T data { get; set; }
        public string Token{get;set;}
    }

}
