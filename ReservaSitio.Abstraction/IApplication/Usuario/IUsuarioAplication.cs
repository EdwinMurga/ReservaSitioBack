﻿using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IApplication.Usuario
{
  public  interface IUsuarioAplication
    {
        public Task<ResultDTO<UsuarioDTO>> RegisterUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> DeleteUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetUsuario(UsuarioDTO request);
        public Task<ResultDTO<UsuarioDTO>> GetListUsuario(UsuarioListDTO request);
    }
}
