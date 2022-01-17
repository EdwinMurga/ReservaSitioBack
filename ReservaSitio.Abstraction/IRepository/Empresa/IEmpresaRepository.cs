using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IRepository.Empresa
{
  public   interface IEmpresaRepository
    {

        public Task<ResultDTO<EmpresaDTO>> RegisterEmpresa(EmpresaDTO request);
        public Task<ResultDTO<EmpresaDTO>> DeleteEmpresa(EmpresaDTO request);
        public Task<ResultDTO<EmpresaDTO>> GetEmpresa(EmpresaDTO request);
        public Task<ResultDTO<EmpresaDTO>> GetListEmpresa(EmpresaDTO request);
    }
}
