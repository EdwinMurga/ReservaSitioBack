using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Auth
{
    public class UserLoginResponseDTO
    {
        public object menu { get; set; }
        public string Token { get; set; }
        public bool Login { get; set; }
        public int idPerfil { get; set; }
        public string nombreCompleto { get; set; }
        public string vcodigo { get; set; }
        public string vcodigoCentro { get; set; }
        public string vcodigoAlmacen { get; set; }
        public string vcorreoElectronico { get; set; }
        public List<string> Errors { get; set; }
    }
}
