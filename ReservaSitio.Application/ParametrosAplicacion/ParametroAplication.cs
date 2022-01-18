using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.ParametrosAplicacion
{
 public   class ParametroAplication: IParametroAplication
    {
        private readonly IParametroServices iParametroServices;
        public ParametroAplication(IParametroServices IParametroServices) 
        {
            this.iParametroServices = IParametroServices;
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> DeleteParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroServices.DeleteParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetListParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroServices.GetListParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> GetParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroServices.GetParametro(request);
        }

        public Task<ResultDTO<ParametroAplicacionDTO>> RegisterParametro(ParametroAplicacionDTO request)
        {
            return this.iParametroServices.RegisterParametro(request);
        }
    }
}
