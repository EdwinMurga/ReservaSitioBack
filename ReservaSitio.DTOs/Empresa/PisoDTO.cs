using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Empresa
{
    public class PisoDTO : AuditoriaDTO
    {

        public int iid_piso {get;set;}
        public int iid_local  {get;set;}
        public string vdescipcion{ get; set; }
        public int iid_nivel{ get; set; }

    }
}
