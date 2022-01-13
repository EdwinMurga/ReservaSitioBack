using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Entities.Opciones
{
    public class Perfil : Auditoria
    {
        public int perfil_iid {get;set;}
        public string perfc_vnombre_perfil {get;set;}
        public string perfc_vdescripcion_perfil{ get; set; }
    }
}
