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
 public   class LocalAplication: ILocalAplication
    {

        private readonly ILocalServices iLocalServices;
        public LocalAplication(ILocalServices ILocalServices) {
            this.iLocalServices = ILocalServices;
        }

        public Task<ResultDTO<LocalDTO>> DeleteLocal(LocalDTO request)
        {
           return this.iLocalServices.DeleteLocal(request);
        }

        public Task<ResultDTO<LocalDTO>> GetListLocal(LocalDTO request)
        {
            return this.iLocalServices.GetListLocal(request);
        }

        public Task<ResultDTO<LocalDTO>> GetLocal(LocalDTO request)
        {
            return this.iLocalServices.GetLocal(request);
        }

        public Task<ResultDTO<LocalDTO>> RegisterLocal(LocalDTO request)
        {
            return this.iLocalServices.RegisterLocal(request);
        }
    }
}
