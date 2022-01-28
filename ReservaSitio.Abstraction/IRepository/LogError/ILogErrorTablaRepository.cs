using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.LogError
{
   public  interface ILogErrorTablaRepository
    {

        public Task<ResultDTO<LogErrorTablaDTO>> RegisterLogTablaError(LogErrorTablaDTO request);

    }
}
