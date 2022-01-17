using ReservaSitio.Abstraction.IRepository.Empresa;
using ReservaSitio.Abstraction.IService.Empresa;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Empresa
{
    public class LocalServices : ILocalServices
    {
        private readonly ILocalRepository iLocaRepository;

      public  LocalServices(ILocalRepository ILocalRepository)
      {
            this.iLocaRepository = ILocalRepository;
      }
     
       public Task<ResultDTO<LocalDTO>> DeleteLocal(LocalDTO request)
       {
            return this.iLocaRepository.DeleteLocal(request);
       }

        public Task<ResultDTO<LocalDTO>> GetListLocal(LocalDTO request)
        {
            return this.iLocaRepository.GetListLocal(request);
        }

        public Task<ResultDTO<LocalDTO>> GetLocal(LocalDTO request)
        {
            return this.iLocaRepository.GetLocal(request);
        }

        public Task<ResultDTO<LocalDTO>> RegisterLocal(LocalDTO request)
        {
            return this.iLocaRepository.RegisterLocal(request);
        }
    }
}
