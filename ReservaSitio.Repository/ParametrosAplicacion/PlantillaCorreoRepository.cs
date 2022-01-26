using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
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

namespace ReservaSitio.Repository.ParametrosAplicacion
{
    public class PlantillaCorreoRepository : BaseRepository,IPlantillaCorreoRepository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public PlantillaCorreoRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

        public async Task<ResultDTO<PlantillaCorreoDTO>> DeletePlantillaCorreo(PlantillaCorreoDTO request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_plantilla_correo", request.iid_plantilla_correo);
                     
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PLANTILLA_CORREO_ELIMINAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    lg.iid_usuario_registra = request.iid_usuario_registra;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }

        public async Task<ResultDTO<PlantillaCorreoDTO>> GetListPlantillaCorreo(PlantillaCorreoDTO request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            List<PlantillaCorreoDTO> list = new List<PlantillaCorreoDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_plantilla_correo", request.iid_plantilla_correo);
                parameters.Add("@p_vnombre_plantilla", request.vnombre_plantilla);
                parameters.Add("@p_vtitulo_correo", request.vtitulo_correo);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);

                parameters.Add("@p_vdescripcion_plantilla", request.vdescripcion_plantilla);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
       

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<PlantillaCorreoDTO>)cn.Query<PlantillaCorreoDTO>("[dbo].[SP_PLANTILLA_CORREO_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = (list.ToList().Count > 0 ? true : false);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
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

        public async Task<ResultDTO<PlantillaCorreoDTO>> GetPlantillaCorreo(PlantillaCorreoDTO request)
        {

            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            PlantillaCorreoDTO item = new PlantillaCorreoDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_plantilla_correo", request.iid_plantilla_correo);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<PlantillaCorreoDTO>("[dbo].[SP_PLANTILLA_CORREO_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PlantillaCorreoDTO)query.FirstOrDefault();
                    res.IsSuccess = (query.Any() == true ? true : false);
                }
                // await mConnection.Complete();
                res.Message = (res.IsSuccess ? UtilMensajes.strInformnacionGrabada : UtilMensajes.strInformnacionNoEncontrada);
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = request.iid_usuario_registra;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PlantillaCorreoDTO>> RegisterPlantillaCorreo(PlantillaCorreoDTO request)
        {
            ResultDTO<PlantillaCorreoDTO> res = new ResultDTO<PlantillaCorreoDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_plantilla_correo", request.iid_plantilla_correo);
                        parameters.Add("@p_vnombre_plantilla", request.vnombre_plantilla);
                        parameters.Add("@p_vrol_para", request.vrol_para);
                        parameters.Add("@p_vrol_cc", request.vrol_cc);
                        parameters.Add("@p_vrol_cco", request.vrol_cco);
                        parameters.Add("@p_vtitulo_correo", request.vtitulo_correo);
                        parameters.Add("@p_vcuerpo_correo", request.vcuerpo_correo);
                        parameters.Add("@p_iid_empresa", request.iid_empresa);
                        parameters.Add("@p_vdescripcion_plantilla", request.vdescripcion_plantilla);

                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PLANTILLA_CORREO_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    lg.iid_usuario_registra = request.iid_usuario_registra;
                    lg.vdescripcion = e.Message.ToString();
                    lg.vcodigo_mensaje = e.Message.ToString();
                    lg.vorigen = this.ToString();
                    await this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
    }
}
