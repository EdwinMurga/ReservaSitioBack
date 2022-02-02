using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Auth
{
    public class AuthenticationResponse
	{
		public int iid_usuario { get; set; }
		public int iid_perfil { get; set; }
		public string perfil { get; set; }
		public int iid_tipo_documento { get; set; }
		public string vtipo_documento { get; set; }
		public string vnro_documento { get; set; }
		public string vnombres { get; set; }
		public string vapellido_paterno { get; set; }
		public string vapellido_materno { get; set; }
		public string vcorreo_electronico { get; set; }
		public string vnumero_telefonico { get; set; }
	 
		public DateTime dfecha_caduca_clave { get; set; }
		//public DateTime dfecha_cambio_clave { get; set; }
		//public int cantidad_intentos { get; set; }
		//public int iid_indica_bloqueo { get; set; }
		public DateTime dfecha_ultimo_acceso { get; set; }
		public int iid_empresa { get; set; }
		public string empresa { get; set; }
	}
}
