using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs
{
   public  class PaginacionDTO
    {
        //[Required(AllowEmptyStrings = true)]
       // public string search { get; set; }
        public int? pageNum { get; set; }
        public int? pageSize { get; set; }
        public int? totalRecord { get; set; }

        public int? totalPage { get; set; }
        public int? startIndex { get; set; }
    }
}
