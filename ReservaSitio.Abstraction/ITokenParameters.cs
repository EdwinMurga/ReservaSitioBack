using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction
{
    public class ITokenParameters
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
        public DateTime FechaCaduca { get; set; }
    }
}
