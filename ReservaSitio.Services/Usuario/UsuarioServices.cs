﻿using ReservaSitio.Abstraction.IRepository;
using ReservaSitio.Abstraction.IService.Usuario;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Usuario
{
    public class UsuarioServices : IUsuarioServices
    {
       private readonly IUsuarioRepository iUsuarioRespository;
        public  UsuarioServices(IUsuarioRepository IUsuarioRespository) {
            this.iUsuarioRespository = IUsuarioRespository;
        }

        public Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request)
        {
          return   this.iUsuarioRespository.DeleteUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request)
        {
            return this.iUsuarioRespository.GetListUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request)
        {
            return this.iUsuarioRespository.GetUsuario(request);
        }

        public Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request)
        {
            return this.iUsuarioRespository.RegisterUsuario(request);
        }
    }
}
