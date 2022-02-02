using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Usuario
{
   public class UsuarioDTO: AuditoriaDTO
	{
		public int iid_usuario { get; set; }
		public int iid_perfil { get; set; }
		public int iid_tipo_documento { get; set; }
		public string vnro_documento { get; set; }
		public string vnombres { get; set; }
		public string vapellido_paterno { get; set; }
		public string vapellido_materno { get; set; }
		public string vcorreo_electronico { get; set; }
		public string vnumero_telefonico { get; set; }
		public string vclave { get; set; }
		public DateTime dfecha_caduca_clave { get; set; }
		public DateTime dfecha_cambio_clave { get; set; }
		public int cantidad_intentos { get; set; }
		public int iid_indica_bloqueo { get; set; }
		public DateTime dfecha_ultimo_acceso { get; set; }
		public int iid_empresa { get; set; }


		public string vnombre_perfil { get; set; }
		public string vestado { get; set; }
		public string vtipodocumento { get; set; }

		public string vcodToken { get; set; }
	}

	public class UsuarioListDTO : AuditoriaDTO
	{
		public int iid_usuario { get; set; }
		public int iid_perfil { get; set; }
		public int iid_tipo_documento { get; set; }
		public string vnro_documento { get; set; }
		public string vnombres { get; set; }
		public string vapellido_paterno { get; set; }
		public string vapellido_materno { get; set; }
		public string vcorreo_electronico { get; set; }
		public string vnumero_telefonico { get; set; }
		//public string vclave { get; set; }
		//public DateTime dfecha_caduca_clave { get; set; }
		//public DateTime dfecha_cambio_clave { get; set; }
		//public int cantidad_intentos { get; set; }
		//public int iid_indica_bloqueo { get; set; }
		public DateTime dfecha_registro_ini { get; set; }
		public DateTime dfecha_registro_fin { get; set; }
		public int iid_empresa { get; set; }


	}


	public class UsuarioRecuperarClave : AuditoriaDTO 
	{
	public int iidrecuperarclave { set; get; }
		public int iid_usuario { set; get; }
		public DateTime dfec_envio_rec_clave { set; get; }
		public DateTime dfec_caduc_recup_clave { set; get; }
		public int iind_estado_recupera_clave { set; get; }
		public DateTime dfec_recupera_clave  { set; get; }
		public string  vtoken { set; get; }
	
	}
}
