using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.LogError
{
   public interface ILogErrorServices
    {
        Task<ResultDTO<LogErrorDTO>> RegisterLogError(LogErrorDTO request);
    }
}
