using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.ParametroAplicacion
{
  public  class ParametroAplicacionDTO
    {
		public int pmtoc_iid_parametro { get; set; }
		public string  pmtoc_vdescripcion { get; set; }
		public string pmtoc_vvalor_cadena { get; set; }
		public string pmtoc_ivalor_entero { get; set; }
		public decimal  pmtoc_nvalor_decimal { get; set; }
		public int  pmtoc_iid_estado_registro { get; set; }
		public int  emprc_iid_empresa { get; set; }

	}
}
