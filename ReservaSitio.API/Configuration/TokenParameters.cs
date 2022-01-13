using ReservaSitio.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        
        public string userName { get; set; }
        public string passwordHash { get; set; }
        public string id { get; set; }
        
    }
}
