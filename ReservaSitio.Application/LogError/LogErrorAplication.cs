using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IService.LogError;
using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.LogError
{
   public class LogErrorAplication : ILogErrorAplication
    {
        private readonly ILogErrorServices iLogErrorServices;
        public LogErrorAplication(ILogErrorServices ILogErrorServices)
        {
            this.iLogErrorServices = ILogErrorServices;
        }

      

        public Task<ResultDTO<LogErrorDTO>> RegisterLogError(LogErrorDTO request)
        {
            return this.iLogErrorServices.RegisterLogError(request);
        }
    }
}
