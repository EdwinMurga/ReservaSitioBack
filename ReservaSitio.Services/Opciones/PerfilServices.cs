using ReservaSitio.Abstraction.IRepository.Perfiles;
using ReservaSitio.Abstraction.IService.Perfiles;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Opciones
{
    public class PerfilServices : IPerfilServices
    {
        private readonly IPerfilRespository iPerfilRespository;
        public PerfilServices(IPerfilRespository IPerfilRespository) { this.iPerfilRespository = IPerfilRespository; }
        public Task<ResultDTO<PerfilDTO>> DeletePerfil(PerfilDTO request)
        {
            return this.iPerfilRespository.DeletePerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> GetListPerfil(PerfilDTO request)
        {
            return this.iPerfilRespository.GetListPerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> GetPerfil(PerfilDTO request)
        {
            return this.iPerfilRespository.GetPerfil(request);
        }

        public Task<ResultDTO<PerfilDTO>> RegisterPerfil(PerfilDTO request)
        {
            return this.iPerfilRespository.RegisterPerfil(request);
        }
    }
}
