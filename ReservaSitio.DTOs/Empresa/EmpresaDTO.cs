using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Empresa
{
   public  class EmpresaDTO:AuditoriaDTO
    {

		public int iid_empresa { get; set; }
		public string vnombre_imagen_logo { get; set; }
		public string vnombre_completo { get; set; }
		public string vruc { get; set; }
		public string vdireccion { get; set; }
		public string vurl { get; set; }
		public string correo_electronico { get; set; }
		public string vtelefono { get; set; }
		public int iid_distrito { get; set; }

	}
}
