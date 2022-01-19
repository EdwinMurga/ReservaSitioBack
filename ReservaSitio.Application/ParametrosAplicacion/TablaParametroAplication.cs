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
    public class TablaParametroAplication : ITablaParametroAplication
    {
        private readonly ITablaParametroServices iITablaParametroServices;
        public TablaParametroAplication(ITablaParametroServices ITablaParametroServices) { this.iITablaParametroServices = ITablaParametroServices; }
        public Task<ResultDTO<TablaDetalleParametroDTO>> DeleteTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
           return  this.iITablaParametroServices.DeleteTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> DeleteTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroServices.DeleteTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> GetListTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroServices.GetListTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> GetListTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroServices.GetListTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> GetTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroServices.GetTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> GetTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroServices.GetTablaParametro(request);
        }

        public Task<ResultDTO<TablaDetalleParametroDTO>> RegisterTablaDetalleParametro(TablaDetalleParametroDTO request)
        {
            return this.iITablaParametroServices.RegisterTablaDetalleParametro(request);
        }

        public Task<ResultDTO<TablaParametroDTO>> RegisterTablaParametro(TablaParametroDTO request)
        {
            return this.iITablaParametroServices.RegisterTablaParametro(request);
        }
    }
}
