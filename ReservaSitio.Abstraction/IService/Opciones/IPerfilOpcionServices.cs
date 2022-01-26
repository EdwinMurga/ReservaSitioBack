using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.Opciones
{
   public interface IPerfilOpcionServices
    {
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO> request);
        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request);
        public Task<ResultDTO<PerfilUsuarioDTO>> GetPerfilOpcionUsuario(PerfilUsuarioDTO request);
    }
}
