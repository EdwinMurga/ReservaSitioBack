using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.Abstraction.IService.Perfiles;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Opciones
{
    public class PerfilAplication: IPerfilAplication
    {
        private readonly IPerfilServices iPerfilServices;
        public  PerfilAplication(IPerfilServices IPerfilServices) { this.iPerfilServices = IPerfilServices; }

        public Task<ResultDTO<PerfilDTO>> DeletePerfil(PerfilDTO request)
        {
            return this.iPerfilServices.DeletePerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> GetListPerfil(PerfilDTO request)
        {
            return this.iPerfilServices.GetListPerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> GetPerfil(PerfilDTO request)
        {
            return this.iPerfilServices.GetPerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> RegisterPerfil(PerfilDTO request)
        {
            return this.iPerfilServices.RegisterPerfil(request);
        }
    }
}
