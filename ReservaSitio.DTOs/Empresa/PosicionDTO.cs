using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Empresa
{
   public  class PosicionDTO:AuditoriaDTO
    {
		public int iid_posicion { set;get; }
		public int iid_piso  { set;get; }
		public int icoordenada_x  { set;get; }
		public int icoordenada_y{ set; get; }
		public int iid_tipo_posicion{ set; get; }
		public int iid_sector{ set; get; }
		public int icapacidad{ set; get; }
		public string videntificador{ set; get; }

	}
}
