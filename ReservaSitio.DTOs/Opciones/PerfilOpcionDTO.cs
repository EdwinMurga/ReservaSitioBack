using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Opciones
{
		public class PerfilOpcionDTO : AuditoriaDTO
	{
		public int iid_perfil_opcion { get; set; }
		public int iid_perfil { get; set; }
		public int iid_opcion { get; set; }
		public string vperfil { get; set; }
		public string vopcion { get; set; }
		public string vmodulo { get; set; }

		public int iacceso_crear { get; set; }
		public int iacceso_actualizar { get; set; }
		public int iacceso_eliminar { get; set; }
		public int iacceso_visualizar { get; set; }


		public bool icrear { get; set; }
		public bool iactualizar { get; set; }
		public bool ieliminar { get; set; }
		public bool ivisualizar { get; set; }

		public bool flg_accesos { get; set; }
	}

		public class PerfilDTOResponse
		{
			public PerfilDTO perfil { get; set; }
			public List<PerfilOpcionDTO> perfilOpcion { get; set; }
		}

		public class PerfilUsuarioResponseDTO
		{
			public int iid_usuario { get; set; }

			public int iid_perfil_opcion { set; get; }
			public int iid_perfil { set; get; }

			public string vnombre_perfil { get; set; }
			//public int iid_opcion { get; set; }
			public int iacceso_crear { get; set; }
			public int iacceso_actualizar { get; set; }
			public int iacceso_eliminar { get; set; }
			public int iacceso_visualizar { get; set; }

			public int iid_sistema { get; set; }
			public int iid_modulo { get; set; }
			public string vtitulo_modulo { get; set; }
			public int iorden_modulo { get; set; }
			public int iindica_visible_modulo { get; set; }

			public string vicono_modulo { get; set; }
			public string vurl_modulo { get; set; }

			public int iid_opcion { get; set; }
			public string vtitulo_opcion { get; set; }
			public int iindica_visible_opcion { get; set; }
			public int iorden_opcion { get; set; }
			public string vicono_opcion { get; set; }
			public string vurl_opcion { get; set; }
		}
		public class PerfilUsuarioDTO
		{
			public int iid_usuario { get; set; }

			public List<ModuloDTO> modulo { set; get; }

			public List<PerfilOpcionDTO> perfilOpcion { get; set; }

		}

		public class Menu
		{
			public int iid_modulo { get; set; }

			public string text { get; set; }
			public string link { get; set; }
			public string icon { get; set; }
			public int iidIndicaSubMenu { get; set; }
			public int iindica_visible_modulo { get; set; }
			public List<SubMenu> submenu { get; set; }
		}
		public class SubMenu
		{
			public int iid_modulo { get; set; }
			public int iid_opcion { get; set; }
			public string text { get; set; }
			public string link { get; set; }
			public string icon { get; set; }

			public int iidIndicaSubMenu { get; set; }
			public int iindica_visible_opcion { get; set; }

			//public int iorden_opcion { get; set; }

			public int icrear { get; set; }
			public int iactualizar { get; set; }
			public int ieliminar { get; set; }
			public int ivisualizar { get; set; }
		}


		public class PerfilUsuarioMenu
		{
			public int iid_usuario { get; set; }
			public List<Menu> menu { set; get; }

		}



	
}