using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Entities.Opciones
{
    public class PerfilOpcion:Auditoria
    {
		public int peopc_iid_perfil_opcion { get; set; }
		public int perfc_iid_perfil { get; set; }
		public int opcic_iid_opcion { get; set; }
		public int peopc_iacceso_crear { get; set; }
		public int peopc_iacceso_actualizar { get; set; }
		public int peopc_iacceso_eliminar { get; set; }
		public int peopc_iacceso_visualizar { get; set; }


	}
}
