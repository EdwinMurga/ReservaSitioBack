using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.Abstraction.IRepository.Perfiles;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.Repository.Opcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using static ReservaSitio.Entities.Enum;

namespace ReservaSitio.Services.Opciones
{
  public  class PerfilOpcionServices: IPerfilOpcionServices
    {
        private readonly IPerfilOpcionRepository iPerfilOpcionRepository;
        private readonly IPerfilRespository iPerfilRepository;
        public PerfilOpcionServices(IPerfilOpcionRepository IPerfilOpcionRepository
            , IPerfilRespository IPerfilRespository) {
            this.iPerfilOpcionRepository = IPerfilOpcionRepository;
            this.iPerfilRepository = IPerfilRespository;
        }

        public Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.DeletePerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.GetListPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request)
        {
            return this.iPerfilOpcionRepository.GetPerfilOpcion(request);
        }

        public Task<ResultDTO<PerfilUsuarioMenu>> GetPerfilOpcionUsuario(PerfilUsuarioMenu request)
        {
            return this.iPerfilOpcionRepository.GetPerfilOpcionUsuario(request);
        }
        public async Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(PerfilDTOResponse request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try 
                {
                 
                   var res_perf= await this.iPerfilRepository.RegisterPerfil(request.perfil);
                    if (!res_perf.IsSuccess) {
                        throw new Exception ("" + res_perf.MessageExeption);
                    }
                    foreach (PerfilOpcionDTO x in request.perfilOpcion  ) {
                        x.iid_perfil = res_perf.Codigo;
                        x.iid_perfil_opcion = (x.flg_accesos == true? 0:x.iid_perfil_opcion);
                        x.iid_usuario_registra = request.perfil.iid_usuario_registra;
                    }

                   var res_peropc = await  this.iPerfilOpcionRepository.RegisterPerfilOpcion(request.perfilOpcion);
                    if (!res_peropc.IsSuccess)
                    {

                        throw new Exception("" + res_perf.MessageExeption);
                    }
                    res.Codigo = res_perf.Codigo;
                    res.IsSuccess = true;
                    res.Message = UtilMensajes.strInformnacionGrabada; ;

                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();
                }
            }



            return res;
        }
        public Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(List<PerfilOpcionDTO>  request)
        {
            return this.iPerfilOpcionRepository.RegisterPerfilOpcion(request);
        }


    }
}
