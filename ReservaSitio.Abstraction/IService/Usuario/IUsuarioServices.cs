using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.Usuario
{
    public interface IUsuarioServices
    {
        public Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request);

        public Task<ResultDTO<UsuarioDTO>> GetUsuarioParameter(UsuarioDTO request);

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioSeguridadInserta(UsuarioDTO request);

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioRecuperaClave(UsuarioDTO request);

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioAcceso(UsuarioDTO request);
       public Task<ResultDTO<UsuarioRecuperarClave>> GetUsuarioRecuperaClave(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioIntentoLogeo(UsuarioDTO request);

    }
}
