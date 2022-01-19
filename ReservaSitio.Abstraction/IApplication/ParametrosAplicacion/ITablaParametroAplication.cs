using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IApplication.ParametrosAplicacion
{
    public interface ITablaParametroAplication
    {
        public Task<ResultDTO<TablaParametroDTO>> RegisterTablaParametro(TablaParametroDTO request);
        public Task<ResultDTO<TablaParametroDTO>> DeleteTablaParametro(TablaParametroDTO request);
        public Task<ResultDTO<TablaParametroDTO>> GetTablaParametro(TablaParametroDTO request);
        public Task<ResultDTO<TablaParametroDTO>> GetListTablaParametro(TablaParametroDTO request);


        public Task<ResultDTO<TablaDetalleParametroDTO>> RegisterTablaDetalleParametro(TablaDetalleParametroDTO request);
        public Task<ResultDTO<TablaDetalleParametroDTO>> DeleteTablaDetalleParametro(TablaDetalleParametroDTO request);
        public Task<ResultDTO<TablaDetalleParametroDTO>> GetTablaDetalleParametro(TablaDetalleParametroDTO request);
        public Task<ResultDTO<TablaDetalleParametroDTO>> GetListTablaDetalleParametro(TablaDetalleParametroDTO request);
    }
}
