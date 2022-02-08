using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;

namespace ReservaSitio.Abstraction.IApplication.Opciones
{
  public   interface IPerfilOpcionAplication
    {
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO> request);

        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(PerfilDTOResponse request);
        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request);

        public Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request);

        public Task<ResultDTO<List<object>>> GetMenuOpcion(int idPerfil);
        public Task<ResultDTO<List<object>>> GetSubMenuOpcion(int idPerfil, int idModulo);
        public Task<List<object>> GetMenu(int idPerfil);
    }
}
