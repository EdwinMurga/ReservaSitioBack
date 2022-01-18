using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.ParametrosAplicacion
{
    public class ParametroServices : IParametroServices
    {
        private readonly IParametroRepository iParametroRepository;
        public ParametroServices(IParametroRepository IParametroRespository) 
        {
            this.iParametroRepository = IParametroRespository;
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroRepository.DeleteParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroRepository.GetListParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroRepository.GetParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroRepository.RegisterParametro(request);
        }
    }
}
