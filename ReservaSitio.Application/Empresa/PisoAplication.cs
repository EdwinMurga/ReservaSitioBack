using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IService.Empresa;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Empresa
{
    public class PisoAplication : IPisoAplication
    {

        private readonly IPisoServices iIPisosServices;

        public PisoAplication(IPisoServices IPisosServices)
        {
            this.iIPisosServices = IPisosServices;
        }

        public Task<ResultDTO<PisoDTO>> DeletePiso(PisoDTO request)
        {
            return this.iIPisosServices.DeletePiso(request);
        }

        public Task<ResultDTO<PisoDTO>> GetListPiso(PisoDTO request)
        {
            return this.iIPisosServices.GetListPiso(request);
        }

        public Task<ResultDTO<PisoDTO>> GetPiso(PisoDTO request)
        {
            return this.iIPisosServices.GetPiso(request);
        }

        public Task<ResultDTO<PisoDTO>> RegisterPiso(PisoDTO request)
        {
            return this.iIPisosServices.RegisterPiso(request);
        }
    }
}
