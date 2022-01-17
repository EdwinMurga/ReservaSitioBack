using ReservaSitio.Abstraction.IRepository.Empresa;
using ReservaSitio.Abstraction.IService.Empresa;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using ReservaSitio.Repository.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Services.Empresa
{
  public  class EmpresaServices: IEmpresaServices
    {
        private readonly IEmpresaRepository iEmpresaRepository;
        public EmpresaServices(IEmpresaRepository IEmpresaRepository)
        {
            this.iEmpresaRepository = IEmpresaRepository;
        }

        public Task<ResultDTO<EmpresaDTO>> DeleteEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaRepository.DeleteEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> GetEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaRepository.GetEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> GetListEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaRepository.GetListEmpresa(request);
        }

        public Task<ResultDTO<EmpresaDTO>> RegisterEmpresa(EmpresaDTO request)
        {
            return this.iEmpresaRepository.RegisterEmpresa(request);
        }
    }
}
