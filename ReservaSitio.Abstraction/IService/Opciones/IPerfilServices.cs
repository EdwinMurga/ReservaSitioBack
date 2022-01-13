using System;
using System.Collections.Generic;
using ReservaSitio.DTOs;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.DTOs.Opciones;

namespace ReservaSitio.Abstraction.IService.Perfiles
{
   public interface IPerfilServices
    {
        public Task<ResultDTO<PerfilDTO>> RegisterPerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> DeletePerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> GetPerfil(PerfilDTO request);
        public Task<ResultDTO<PerfilDTO>> GetListPerfil(PerfilDTO request);
    }
}
