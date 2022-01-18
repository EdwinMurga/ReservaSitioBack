using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.ParametroAplicacion
{
 public   class PlantillaCorreoDTO:AuditoriaDTO
    {

	public int iid_plantilla_correo { get; set; }
	public string vnombre_plantilla { get; set; }
		public string vrol_para { get; set; }
		public string vrol_cc { get; set; }
		public string vrol_cco { get; set; }
		public string _vtitulo_correo { get; set; }
		public string vcuerpo_correo { get; set; }


	public string vdescripcion_plantilla { get; set; }

	public int iid_empresa { get; set; }

}
}
