using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Entities.Opciones
{
    public class Modulo:Auditoria
    {
		// { get; set; }
		public int modulo_iid { get; set; }
		public int sistc_iid_sistema { get; set; }
		public string moducc_vtitulo{ get; set; }
		public string moducc_vurl		{ get; set; }
		public string moducc_vicono		{ get; set; }
		public int moduc_iindica_sub_menu{ get; set; }
		public int moduc_iindica_visible{ get; set; }
		public int moduc_iorden{ get; set; }

	}
}
