using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.Perfiles
{
    public interface IPerfilRespository
    {
        public Task<ResultDTO<PerfilDTO>> RegisterPerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> DeletePerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> GetPerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> GetListPerfil(PerfilDTO request);
    }
}
