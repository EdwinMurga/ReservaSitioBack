using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IService.ParametrosAplicacion
{
public    interface IPlantillaCorreoServices
    {

        public Task<ResultDTO<PlantillaCorreoDTO>> RegisterPlantillaCorreo(PlantillaCorreoDTO request);
        public Task<ResultDTO<PlantillaCorreoDTO>> DeletePlantillaCorreo(PlantillaCorreoDTO request);
        public Task<ResultDTO<PlantillaCorreoDTO>> GetPlantillaCorreo(PlantillaCorreoDTO request);
        public Task<ResultDTO<PlantillaCorreoDTO>> GetListPlantillaCorreo(PlantillaCorreoDTO request);
    }

}
