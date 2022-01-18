using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.ParametrosAplicacion
{
  public  class PlantillaCorreoAplication: IPlantillaCorreoAplication
    {
        private readonly IPlantillaCorreoServices iPlantillaCorreoAplication;
        public PlantillaCorreoAplication(IPlantillaCorreoServices IPlantillaCorreoServices) 
        {
            this.iPlantillaCorreoAplication = IPlantillaCorreoServices;
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> DeletePlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoAplication.DeletePlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> GetListPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoAplication.GetListPlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> GetPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoAplication.GetPlantillaCorreo(request);
        }

        public Task<ResultDTO<PlantillaCorreoDTO>> RegisterPlantillaCorreo(PlantillaCorreoDTO request)
        {
            return this.iPlantillaCorreoAplication.RegisterPlantillaCorreo(request);
        }
    }
}
