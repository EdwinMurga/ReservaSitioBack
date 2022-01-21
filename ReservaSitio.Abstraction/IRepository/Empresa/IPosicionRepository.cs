using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.Empresa
{
    public interface IPosicionRepository
    {
        public Task<ResultDTO<PosicionDTO>> RegisterPosicion(PosicionDTO request);
        public Task<ResultDTO<PosicionDTO>> DeletePosicion(PosicionDTO request);
        public Task<ResultDTO<PosicionDTO>> GetPosicion(PosicionDTO request);
        public Task<ResultDTO<PosicionDTO>> GetListPosicion(PosicionDTO request);
    }
}
