using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.Abstraction.IRepository.Perfiles;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Opciones
{
    public class OpcionServices: IOpcionServices
    {
        private readonly IOpcionRepository iOpcionRespository;

        public  OpcionServices(IOpcionRepository IOpcionRepository) 
        {
            this.iOpcionRespository = IOpcionRepository;
        }

        public Task<ResultDTO<OpcionDTO>> DeleteOpcion(OpcionDTO request)
        {
          return  this.iOpcionRespository.DeleteOpcion(request);
        }

        public Task<ResultDTO<OpcionDTO>> GetListOpcion(OpcionDTO request)
        {
            return this.iOpcionRespository.GetListOpcion(request);
        }

        public Task<ResultDTO<OpcionPerfilDTO>> GetListOpcionByModulo(ModuloDTO request)
        {
            return this.iOpcionRespository.GetListOpcionByModulo(request);
        }

        public Task<ResultDTO<OpcionDTO>> GetOpcion(OpcionDTO request)
        {
            return this.iOpcionRespository.GetOpcion(request);
        }

        public Task<ResultDTO<OpcionDTO>> RegisterOpcion(OpcionDTO request)
        {
            return this.iOpcionRespository.RegisterOpcion(request);
        }
    }
}
