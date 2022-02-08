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
  public   class PerfilOpcionAplication: IPerfilOpcionAplication
    {
        private readonly IPerfilOpcionServices iPerfilOpcionServices;
        public PerfilOpcionAplication(IPerfilOpcionServices IPerfilOpcionServices) {
            this.iPerfilOpcionServices = IPerfilOpcionServices;
        }

        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionServices.DeletePerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionServices.GetListPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionServices.GetPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
        {
            return this.iPerfilOpcionServices.GetPerfilOpcionUsuario(request);
        }
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(PerfilDTOResponse request)
        {
            return this.iPerfilOpcionServices.RegisterPerfilOpcion(request);
        }
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO> request)
        {
            return this.iPerfilOpcionServices.RegisterPerfilOpcion(request);
        }

        public Task<ResultDTO<List<object>>> GetMenuOpcion(int IdPerfil)
        {
            return this.iPerfilOpcionServices.GetMenuOpcion(IdPerfil);
        }

        public Task<ResultDTO<List<object>>> GetSubMenuOpcion(int IdPerfil, int IdModulo)
        {
            return this.iPerfilOpcionServices.GetSubMenuOpcion(IdPerfil, IdModulo);
        }

        public async Task<List<object>> GetMenu(int IdPerfil)
        {
            return await this.iPerfilOpcionServices.GetMenu(IdPerfil);
        }
    }
}
