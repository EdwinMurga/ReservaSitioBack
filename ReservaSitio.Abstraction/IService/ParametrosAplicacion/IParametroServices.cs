using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.ParametrosAplicacion
{
   public interface IParametroServices
    {
        public Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request);
        public Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request);
    }
}
