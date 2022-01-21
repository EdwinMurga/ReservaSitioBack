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
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request);

        public Task<ResultDTO<PerfilUsuarioDTO>> GetPerfilOpcionUsuario(PerfilUsuarioDTO request);
    }
}
