using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ReservaSitio.Entities.Enum;

namespace ReservaSitio.Repository.Opcion
{
    public class ModuloRepository: BaseRepository, IModuloRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public ModuloRepository(ICustomConnection connection
            , IConfiguration configuration
             , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        public async Task<ResultDTO<ModuloDTO>> RegisterModulo(ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_modulo", request.iid_modulo);
                        parameters.Add("@p_iid_sistema", request.iid_sistema);
                        parameters.Add("@p_vtitulo", request.vtitulo);
                        parameters.Add("@p_iindica_visible", request.iindica_visible);
                        parameters.Add("@p_iorden", request.iorden);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_MODULO_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                res.IsSuccess = true;
                                res.Message = UtilMensajes.strInformnacionGrabada;
                            }
                        }
                        await mConnection.Complete();
                    }
                 

                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = 0;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
        public async Task<ResultDTO<ModuloDTO>> DeleteModulo(ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = new SqlConnection(_connectionString))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_vcodigo_cliente", request.iid_modulo);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_MODULO_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                res.IsSuccess = (res.Codigo == 0 ? false : true);
                                res.Message = (res.Codigo == 0 ? UtilMensajes.strInformnacionNoGrabada : UtilMensajes.strInformnacionGrabada);
                            }
                        }
                        await mConnection.Complete();
                    }


                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();

                    LogErrorDTO lg = new LogErrorDTO();
                    lg.iid_usuario_registra = 0;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
           
            return res;
        }
        public async Task<ResultDTO<ModuloDTO>> GetModulo(ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            ModuloDTO item = new ModuloDTO();
            
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_modulo", request.iid_modulo);
                    using (var cn = new SqlConnection(_connectionString))
                    {
                        var query =await cn.QueryAsync<ModuloDTO>("[dbo].[SP_MODULO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                         item = (ModuloDTO) query.FirstOrDefault();
                         res.IsSuccess = (query.Any() == true ? true : false);
                }
               // await mConnection.Complete();
                
                res.Message = (res.IsSuccess? UtilMensajes.strInformnacionGrabada: UtilMensajes.strInformnacionNoEncontrada);
                res.item = item;
                }
                catch (Exception e)
                {
                   
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            
            return res;
        }
        public async Task<ResultDTO<ModuloDTO>> GetListModulo(ModuloDTO request)
        {
            ResultDTO<ModuloDTO> res = new ResultDTO<ModuloDTO>();
            List<ModuloDTO> list = new List<ModuloDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_modulo", request.iid_modulo);
                parameters.Add("@p_iid_sistema", request.iid_sistema);
                parameters.Add("@p_vtitulo", request.vtitulo);
                parameters.Add("@p_iindica_visible", request.iindica_visible);
                parameters.Add("@p_iorden", request.iorden);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {

                    list = (List<ModuloDTO>)cn.Query<ModuloDTO>("[dbo].[SP_MODULO_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);

                }
                res.IsSuccess = (list.ToList().Count>0?true:false);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);  
                res.data = list.ToList();
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = 0;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

    }
}
