using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Entities.Autenticacion
{
    public class AUTENTICACION_OBJ
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public int idPerfil { get; set; }
        public string nombreCompleto { get; set; }
        public List<string> Errors { get; set; }
    }
}
