using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.ParametroAplicacion
{
  public  interface IParametroRepository
    {

        public Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request);

    }
}
