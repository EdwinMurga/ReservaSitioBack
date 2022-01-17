using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Repository.ParametrosAplicacion
{
    public class ParametroRespository: IParametroRespository
    {

        public ParametroRespository() { }

        public Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
