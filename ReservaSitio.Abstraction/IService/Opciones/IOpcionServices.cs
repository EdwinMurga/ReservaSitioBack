﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;

namespace ReservaSitio.Abstraction.IService.Opciones
{
    public interface IOpcionServices
    {
        public Task<ResultDTO<OpcionDTO>> RegisterOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> DeleteOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> GetOpcion(OpcionDTO request);
        public Task<ResultDTO<OpcionDTO>> GetListOpcion(OpcionDTO request);

    }
}