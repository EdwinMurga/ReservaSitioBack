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
    public class PisoServices : IPisoServices
    {

        private readonly IPisoRepository iIPisoRepository;

        public PisoServices(IPisoRepository IPisoRepository) 
        {
            this.iIPisoRepository = IPisoRepository;
        }

        public Task<ResultDTO<PisoDTO>> DeletePiso(PisoDTO request)
        {
            return this.iIPisoRepository.DeletePiso(request);
        }

        public Task<ResultDTO<PisoDTO>> GetListPiso(PisoDTO request)
        {
            return this.iIPisoRepository.GetListPiso(request);
        }

        public Task<ResultDTO<PisoDTO>> GetPiso(PisoDTO request)
        {
            return this.iIPisoRepository.GetPiso(request);
        }

        public Task<ResultDTO<PisoDTO>> RegisterPiso(PisoDTO request)
        {
            return this.iIPisoRepository.RegisterPiso(request);
        }
    }
}
