using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository
{
  public   interface IUsuarioRepository
    {
        public Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request);

        public Task<ResultDTO<UsuarioDTO>> GetUsuarioParameter(UsuarioDTO request);

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioAcceso(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioIntentoLogeo(UsuarioDTO request);
    }
}
