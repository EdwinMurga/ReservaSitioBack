using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Opciones
{
    public class PerfilOpcionDTO: AuditoriaDTO
	{
		public int iid_perfil_opcion { get; set; }
		public int iid_perfil { get; set; }
		public int iid_opcion { get; set; }
		public int iacceso_crear { get; set; }
		public int iacceso_actualizar { get; set; }
		public int iacceso_eliminar { get; set; }
		public int iacceso_visualizar { get; set; }


	}
}
