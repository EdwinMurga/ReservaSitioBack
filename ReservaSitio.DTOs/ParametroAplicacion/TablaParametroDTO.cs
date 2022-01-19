using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.ParametroAplicacion
{
   public  class TablaParametroDTO:AuditoriaDTO
    {
        public int iid_tabla_auxiliar { get; set; }
	public string vdescripcion { get; set; }
        public int iindica_agregacion { get; set; }
    }
}
