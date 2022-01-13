using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Opciones
{
    public class ModuloServices: IModuloServices
    {
        private readonly IModuloRepository iModuloRespository;

        public  ModuloServices(IModuloRepository IModuloRespository) { this.iModuloRespository = IModuloRespository; }

        public Task<ResultDTO<ModuloDTO>> DeleteModulo(ModuloDTO request)
        {
            return this.iModuloRespository.DeleteModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> GetListModulo(ModuloDTO request)
        {
            return this.iModuloRespository.GetListModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> GetModulo(ModuloDTO request)
        {
            return this.iModuloRespository.GetModulo(request);
        }

        public Task<ResultDTO<ModuloDTO>> RegisterModulo(ModuloDTO request)
        {
            return this.iModuloRespository.RegisterModulo(request);
        }
    }
}
