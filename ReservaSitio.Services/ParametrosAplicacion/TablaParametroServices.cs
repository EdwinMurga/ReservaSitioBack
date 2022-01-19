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
    public class TablaParametroServices : ITablaParametroServices
    {
        private readonly ITablaParametroRepository iITablaParametroRepository;


        public TablaParametroServices(ITablaParametroRepository ITablaParametroRespository) {
            this.iITablaParametroRepository = ITablaParametroRespository;
        }
        public Task<ResultDTO<TablaDetalleParametroDTO>> DeleteTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroRepository.DeleteTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> DeleteTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroRepository.DeleteTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> GetListTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
          return  this.iITablaParametroRepository.GetListTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> GetListTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroRepository.GetListTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> GetTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroRepository.GetTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> GetTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroRepository.GetTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> RegisterTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroRepository.RegisterTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> RegisterTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroRepository.RegisterTablaParametro(request);
        }
    }
}
