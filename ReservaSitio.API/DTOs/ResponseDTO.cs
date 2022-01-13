using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.DTOs
{
    public class ResponseDTO
    {
        public int status { get; set; }
        public string description { get; set; }
        public object objModel { get; set; }
        public string token { get; set; }

        public ResponseDTO Success(ResponseDTO obj, object objModel)
        {
            obj.description = (obj.description=="" || obj.description==null? "Transaction Sucessfully":obj.description);
            obj.status = 1;
            obj.objModel = objModel;
            obj.token = token;
            return obj;
        }

        public ResponseDTO Failed(ResponseDTO obj, string e)
        {
            obj.description = e;
            obj.status = 0;
            return obj;
        }
    }
}
