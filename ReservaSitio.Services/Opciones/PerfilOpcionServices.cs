using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.Repository.Opcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Opciones
{
  public  class PerfilOpcionServices: IPerfilOpcionServices
    {
        private readonly IPerfilOpcionRepository iPerfilOpcionRepository;

        public PerfilOpcionServices(IPerfilOpcionRepository IPerfilOpcionRepository) {
            this.iPerfilOpcionRepository = IPerfilOpcionRepository;
        }

        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.DeletePerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.GetListPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.GetPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
        {
            return this.iPerfilOpcionRepository.GetPerfilOpcionUsuario(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO>  request)
        {
            return this.iPerfilOpcionRepository.RegisterPerfilOpcion(request);
        }


    }
}
