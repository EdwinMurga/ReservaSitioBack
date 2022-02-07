using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Opciones
{
  public  class OpcionAplication: IOpcionAplication
    {
        private readonly IOpcionServices iOpcionServices;
        public OpcionAplication(IOpcionServices iOpcionServices)
        {
            this.iOpcionServices = iOpcionServices;
        }

        public Task<ResultDTO<OpcionDTO>> DeleteOpcion(OpcionDTO request)
        {
            return this.iOpcionServices.DeleteOpcion(request);
        }

        public Task<ResultDTO<OpcionDTO>> GetListOpcion(OpcionDTO request)
        {
            return this.iOpcionServices.GetListOpcion(request);
        }

        public Task<ResultDTO<OpcionPerfilDTO>> GetListOpcionByModulo(ModuloDTO request)
        {
            return this.iOpcionServices.GetListOpcionByModulo(request);
        }

        public Task<ResultDTO<OpcionDTO>> GetOpcion(OpcionDTO request)
        {
            return this.iOpcionServices.GetOpcion(request);
        }

        public Task<ResultDTO<OpcionDTO>> RegisterOpcion(OpcionDTO request)
        {
          return  this.iOpcionServices.RegisterOpcion(request);
        }
    }
}
