using ReservaSitio.Abstraction.IApplication.Usuario;
using ReservaSitio.Abstraction.IService.Usuario;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Usuario
{
    public class UsuarioAplication : IUsuarioAplication
    {
        private readonly IUsuarioServices iIUsuarioServices;
        public UsuarioAplication(IUsuarioServices IIUsuarioServices)
        {
            this.iIUsuarioServices = IIUsuarioServices;
        }

        public Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request)
        {
            return this.iIUsuarioServices.DeleteUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request)
        {
            return this.iIUsuarioServices.GetListUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request)
        {
            return this.iIUsuarioServices.GetUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request)
        {
            return this.iIUsuarioServices.RegisterUsuario(request);
        }



        public Task<ResultDTO<UsuarioDTO>> GetUsuarioParameter(UsuarioDTO request)
        {
            return this.iIUsuarioServices.GetUsuarioParameter(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioAcceso(UsuarioDTO request)
        {
            return this.iIUsuarioServices.RegisterUsuarioAcceso(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioIntentoLogeo(UsuarioDTO request)
        {
            return this.iIUsuarioServices.RegisterUsuarioIntentoLogeo(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioSeguridadInserta(UsuarioDTO request)
        {
            return this.iIUsuarioServices.RegisterUsuarioSeguridadInserta(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuarioRecuperaClave(UsuarioDTO request)
        {
            return this.iIUsuarioServices.RegisterUsuarioRecuperaClave(request);
        }

        public Task<ResultDTO<UsuarioRecuperarClave>> GetUsuarioRecuperaClave(UsuarioDTO request)
        {
            return this.iIUsuarioServices.GetUsuarioRecuperaClave(request);
        }
    }
    
    
}
