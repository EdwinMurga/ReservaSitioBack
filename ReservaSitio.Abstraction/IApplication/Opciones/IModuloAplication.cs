using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;

namespace ReservaSitio.Abstraction.IApplication.Opciones
{
  public  interface IModuloAplication
    {
        public Task<ResultDTO<ModuloDTO>> RegisterModulo(ModuloDTO request);
        public Task<ResultDTO<ModuloDTO>> DeleteModulo(ModuloDTO request);
        public Task<ResultDTO<ModuloDTO>> GetModulo(ModuloDTO request);
        public Task<ResultDTO<ModuloDTO>> GetListModulo(ModuloDTO request);
    }
}
