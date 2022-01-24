using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSitio.API.Controllers.Empresa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaAplication iEmpresaAplication;
        private readonly ILogErrorAplication iLogErrorAplication;
        public EmpresaController(IEmpresaAplication IEmpresaAplication
            , ILogErrorAplication ILogErrorAplication) 
        {
            this.iEmpresaAplication = IEmpresaAplication;
            this.iLogErrorAplication = ILogErrorAplication;
        }

        [HttpPost]
        [Route("RegisterEmpresa")]
        public async Task<ActionResult> RegisterEmpresa([FromBody] EmpresaDTO request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            try
            {
                res = await this.iEmpresaAplication.RegisterEmpresa(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListEmpresa")]
        public async Task<ActionResult> GetListEmpresa([FromBody] EmpresaDTO request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            try
            {
                res = await this.iEmpresaAplication.GetListEmpresa(request);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("GetEmpresa")]
        public async Task<ActionResult> GetEmpresa([FromQuery] int request)
        {
            ResultDTO<EmpresaDTO> res = new ResultDTO<EmpresaDTO>();
            try
            {
                EmpresaDTO item = new EmpresaDTO();
                item.iid_empresa = request;
                res = await this.iEmpresaAplication.GetEmpresa(item);
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
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = sorigen;
                await this.iLogErrorAplication.RegisterLogError(lg);

                return BadRequest(res);
            }
        }
    }   
}
