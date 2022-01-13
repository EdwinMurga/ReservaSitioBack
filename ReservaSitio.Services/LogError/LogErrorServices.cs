using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IService.LogError;
using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.LogError
{
    public class LogErrorServices: ILogErrorServices
    {
        private readonly ILogErrorRepository iLogErrorRepository;
        public  LogErrorServices(ILogErrorRepository ILogErrorRepository) 
        {
            this.iLogErrorRepository = ILogErrorRepository;
        }

        public Task<ResultDTO<LogErrorDTO>> RegisterLogError(LogErrorDTO request)
        {
            return this.iLogErrorRepository.RegisterLogError(request);
        }
    }
}
