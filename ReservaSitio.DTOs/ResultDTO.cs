
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs
{
   public class ResultDTO<T>
    {
		public	int Codigo { get; set; }
		public Boolean IsSuccess { get; set; }
		public string Message { get; set; }
		public string MessageExeption { get; set; }
		public string StackTrace { get; set; }
		public string InnerException { get; set; }
		public string Informacion { get; set; }
		public List<T> data { get; set; }
		public T item { get; set; }

		public byte[] file { get; set; }
		public int totalregistro { get; set; }
	}

}
