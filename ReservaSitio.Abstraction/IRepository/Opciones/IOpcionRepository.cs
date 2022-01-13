using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.Opciones
{
  public  interface IOpcionRepository
    {

        public Task<ResultDTO<OpcionDTO>> RegisterOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> DeleteOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> GetOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> GetListOpcion(OpcionDTO request);

    }
}
