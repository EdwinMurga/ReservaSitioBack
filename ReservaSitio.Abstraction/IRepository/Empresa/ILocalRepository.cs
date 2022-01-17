﻿using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.Empresa
{
 public   interface ILocalRepository
    {

        public Task<ResultDTO<LocalDTO>> RegisterLocal(LocalDTO request);
        public Task<ResultDTO<LocalDTO>> DeleteLocal(LocalDTO request);
        public Task<ResultDTO<LocalDTO>> GetLocal(LocalDTO request);
        public Task<ResultDTO<LocalDTO>> GetListLocal(LocalDTO request);
    }
}
