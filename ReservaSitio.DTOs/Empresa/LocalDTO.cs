using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Empresa
{
 public   class LocalDTO:AuditoriaDTO
    {
		public int iid_local { get; set; }
		public string vdescripcion { get; set; }
		public string vdireccion { get; set; }
		public string icantidad_pisos { get; set; }
		public int iid_distrito { get; set; }
		public int iid_empresa { get; set; }
	}
}
