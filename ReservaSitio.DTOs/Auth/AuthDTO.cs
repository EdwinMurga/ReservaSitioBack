using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.DTOs
{
    public class LoginDTO
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string GoogleToken { get; set; }
    }
 

    public class LoginRecuperacionDTO
    {
        [Required]
        public string GoogleToken { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

       // public string codigo { get; set; }

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
