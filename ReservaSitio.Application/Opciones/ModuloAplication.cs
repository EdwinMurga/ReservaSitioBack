using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Opciones
{
   public class ModuloAplication: IModuloAplication
    {
        private readonly IModuloServices iModuloServices;
        public ModuloAplication(IModuloServices IModuloServices) 
        { 
            this.iModuloServices = IModuloServices;
        }

        public Task<ResultDTO<ModuloDTO>> DeleteModulo(ModuloDTO request)
        {
          return  this.iModuloServices.DeleteModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> GetListModulo(ModuloDTO request)
        {
            return this.iModuloServices.GetListModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> GetModulo(ModuloDTO request)
        {
            return this.iModuloServices.GetModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> RegisterModulo(ModuloDTO request)
        {
            return this.iModuloServices.RegisterModulo(request);
        }
    }
}
