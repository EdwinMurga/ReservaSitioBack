using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.ParametroAplicacion
{
  public  class ParametroAplicacionDTO:AuditoriaDTO
    {
		public int iid_parametro { get; set; }
		public string  vdescripcion { get; set; }
		public string vvalor_cadena { get; set; }
		public string ivalor_entero { get; set; }
		public decimal  nvalor_decimal { get; set; }
		public int  iid_empresa { get; set; }

	}
}
