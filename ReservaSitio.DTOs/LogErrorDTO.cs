using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs
{
   public class LogErrorDTO:AuditoriaDTO
	{

	public int	iid_error {get;set;}
	public int iid_opcion {get;set;}
	public int ierror_number{ get; set; }
	public string vdescripcion { get; set; }
	public int ierror_line {get; set;}
	public int iid_tipo_mensaje{ get; set; }
	public string vcodigo_mensaje { get; set; }
	public string vorigen { get; set; }
}
}
