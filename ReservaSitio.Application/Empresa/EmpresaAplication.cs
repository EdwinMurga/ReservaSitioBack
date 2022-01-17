using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IService.Empresa;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Empresa
{
   public class EmpresaAplication: IEmpresaAplication
    {
        private readonly IEmpresaServices iEmpresaServices;

        public EmpresaAplication(IEmpresaServices IEmpresaServices)
        {
            this.iEmpresaServices = IEmpresaServices;
        }

        public Task<ResultDTO<EmpresaDTO>> DeleteEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaServices.DeleteEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> GetEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaServices.GetEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> GetListEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaServices.GetListEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> RegisterEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaServices.RegisterEmpresa(request);
        }
    }
}
