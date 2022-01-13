using ReservaSitio.DTOs;
using ReservaSitio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReservaSitio.Entities.Opciones
{
	public class OpcionDTO : AuditoriaDTO
	{
		public int opcion_iid {get;set;}
		public int opcic_iid_modulo { get;set;}
		public int opcic_iorden { get;set;}
		public string opcic_vtitulo { get; set; }
		public string opcic_vurl { get; set; }
		public int opcic_iindica_visible { get; set; }

	}
}
