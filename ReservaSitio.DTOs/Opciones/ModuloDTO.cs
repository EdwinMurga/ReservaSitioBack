using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Opciones
{
    public class ModuloDTO: AuditoriaDTO
	{
		// { get; set; }
		public int iid_modulo { get; set; }
		public int iid_sistema { get; set; }
		public string vtitulo{ get; set; }
		public string vurl		{ get; set; }
		public string vicono		{ get; set; }
		public int iindica_sub_menu{ get; set; }
		public int iindica_visible{ get; set; }
		public int iorden{ get; set; }

		public List<OpcionDTO> opcion { set; get; }

	}
}
