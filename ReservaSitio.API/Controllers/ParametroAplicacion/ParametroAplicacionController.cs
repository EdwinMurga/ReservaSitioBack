using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.API.DTO;
using ReservaSitio.Application.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.DTOs.ParametroAplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.ParametroAplicacion
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParametroAplicacionController : Controller
    {

        private readonly IParametroAplication iParametroAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        private readonly ITablaParametroAplication iITablaParametroAplication;

        public ParametroAplicacionController(IParametroAplication _iIPerfilAplication,
            ILogErrorAplication ILogErrorAplication,
            ITablaParametroAplication ITablaParametroAplication)
        {
            this.iParametroAplication = _iIPerfilAplication;
            this.iLogErrorAplication = ILogErrorAplication;
            this.iITablaParametroAplication = ITablaParametroAplication;
        }

        #region "Parametro Globales"

        [HttpDelete]
        //[Route("DeleteParametro")]
        public async Task<ActionResult> DeleteParametro([FromQuery] int request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                ParametroAplicacionDTO item = new ParametroAplicacionDTO();
                item.iid_parametro = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iParametroAplication.DeleteParametro(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterParametro")]
        public async Task<ActionResult> RegisterParametro([FromBody] ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iParametroAplication.RegisterParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListParametro")]
        public async Task<ActionResult> GetListParametro([FromBody] ParametroAplicacionDTO request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iParametroAplication.GetListParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetParametro")]
        public async Task<ActionResult> GetParametro([FromQuery] int request)
        {
            ResultDTO<ParametroAplicacionDTO> res = new ResultDTO<ParametroAplicacionDTO>();
            try
            {
                ParametroAplicacionDTO item = new ParametroAplicacionDTO();
                item.iid_parametro = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iParametroAplication.GetParametro(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }

        }

        #endregion

        #region "tabla parametros Globales"
       

        [HttpPost]
        [Route("RegisterTablaParametro")]
        public async Task<ActionResult> RegisterTablaParametro([FromBody] TablaParametroDTO request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iITablaParametroAplication.RegisterTablaParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListTablaParametro")]
        public async Task<ActionResult> GetListTablaParametro([FromBody] TablaParametroDTO request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iITablaParametroAplication.GetListTablaParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetTablaParametro")]
        public async Task<ActionResult> GetTablaParametro([FromQuery] int request)
        {
            ResultDTO<TablaParametroDTO> res = new ResultDTO<TablaParametroDTO>();
            try
            {
                TablaParametroDTO item = new TablaParametroDTO();
                item.iid_tabla_auxiliar = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iITablaParametroAplication.GetTablaParametro(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }

        }


        #endregion


        #region "tabla detalle parametros Globales"

        [HttpDelete]
        [Route("DeleteTablaDetalleParametro")]
        public async Task<ActionResult> DeleteTablaDetalleParametro([FromQuery] int request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            try
            {
                TablaDetalleParametroDTO item = new TablaDetalleParametroDTO();
                item.iid_tabla_auxiliar = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iITablaParametroAplication.DeleteTablaDetalleParametro(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterTablaDetalleParametro")]
        public async Task<ActionResult> RegisterTablaDetalleParametro([FromBody] TablaDetalleParametroDTO request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iITablaParametroAplication.RegisterTablaDetalleParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListTablaDetalleParametro")]
        public async Task<ActionResult> GetListTablaDetalleParametro([FromBody] TablaDetalleParametroDTO request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            try
            {
                request.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iITablaParametroAplication.GetListTablaDetalleParametro(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetListCbTablaDetalleParametro")]
        public async Task<ActionResult> GetListCbTablaDetalleParametro([FromQuery] int requestAuxiliar)
        {
            ResultDTO<ListCbDTO> res = new ResultDTO<ListCbDTO>();
            try
            {
                TablaDetalleParametroDTO req = new TablaDetalleParametroDTO();
                req.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                req.iid_tabla_auxiliar = requestAuxiliar;
                req.pageNum = 1;
                req.pageSize = 10000;
                req.iid_codigo_descripcion = "";
                req.vvalor_texto_corto = "";
                req.iid_usuario_registra = -1;
                req.iid_estado_registro = -1;

                ResultDTO<TablaDetalleParametroDTO> restabl = await this.iITablaParametroAplication.GetListTablaDetalleParametro(req);

                if (restabl.IsSuccess)
                {
                    List<ListCbDTO> list = new List<ListCbDTO>();
                    foreach (TablaDetalleParametroDTO x in restabl.data)
                    {
                        ListCbDTO item = new ListCbDTO();
                        item.id = x.nvalor_entero;
                        item.value = x.vvalor_texto_corto;
                        item.value2 = x.vvalor_texto_largo;
                        list.Add(item);
                    }
                    res.data = list;
                    res.IsSuccess = restabl.IsSuccess;
                    res.Message = restabl.Message;
                    res.InnerException = restabl.InnerException;
                }


                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetTablaDetalleParametro")]
        public async Task<ActionResult> GetTablaDetalleParametro([FromQuery] int request)
        {
            ResultDTO<TablaDetalleParametroDTO> res = new ResultDTO<TablaDetalleParametroDTO>();
            try
            {
                TablaDetalleParametroDTO item = new TablaDetalleParametroDTO();
                item.iid_tabla_detalle = request;
                item.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                res = await this.iITablaParametroAplication.GetTablaDetalleParametro(item);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                var sorigen = "";
                foreach (object c in this.ControllerContext.RouteData.Values.Values)
                {
                    sorigen += c.ToString() + " | ";
                }
                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }

        }


        #endregion
    }
}
