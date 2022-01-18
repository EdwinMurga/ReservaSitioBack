using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.ParametrosAplicacion
{
   public class PlantillaCorreoServices: IPlantillaCorreoServices
    {
        private readonly IPlantillaCorreoRepository iPlantillaCorreoRepository;
        public PlantillaCorreoServices(IPlantillaCorreoRepository IPlantillaCorreoRepository)
        {
            this.iPlantillaCorreoRepository = IPlantillaCorreoRepository;
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> DeletePlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoRepository.DeletePlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> GetListPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoRepository.GetListPlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> GetPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoRepository.GetPlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> RegisterPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoRepository.RegisterPlantillaCorreo(request);
        }
    }
}
