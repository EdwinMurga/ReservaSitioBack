using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IApplication.LogError
{
  public  interface ILogErrorAplication
    {
        Task<ResultDTO<LogErrorDTO>> RegisterLogError(LogErrorDTO request);
    }
}
