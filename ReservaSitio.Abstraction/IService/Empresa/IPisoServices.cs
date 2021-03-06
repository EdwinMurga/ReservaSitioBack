using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.Empresa
{
    public interface IPisoServices
    {
        public Task<ResultDTO<PisoDTO>> RegisterPiso(PisoDTO request);
        public Task<ResultDTO<PisoDTO>> DeletePiso(PisoDTO request);
        public Task<ResultDTO<PisoDTO>> GetPiso(PisoDTO request);
        public Task<ResultDTO<PisoDTO>> GetListPiso(PisoDTO request);
    }
}
