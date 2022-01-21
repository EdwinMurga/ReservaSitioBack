using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.Empresa;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Empresa;
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

namespace ReservaSitio.Repository.Empresa
{
    public class PosicionRepository : BaseRepository,IPosicionRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public PosicionRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
        public Task<ResultDTO<PosicionDTO>> DeletePosicion(PosicionDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<PosicionDTO>> GetListPosicion(PosicionDTO request)
        {
            ResultDTO<PosicionDTO> res = new ResultDTO<PosicionDTO>();
            List<PosicionDTO> list = new List<PosicionDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_posicion", request.iid_posicion);
                parameters.Add("@p_iid_piso", request.iid_piso);
                parameters.Add("@p_iid_tipo_posicion", request.iid_tipo_posicion);
                parameters.Add("@p_iid_sector", request.iid_sector);
                parameters.Add("@p_icapacidad", request.icapacidad);

                parameters.Add("@p_videntificador", request.videntificador);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<PosicionDTO>)cn.Query<PosicionDTO>("[dbo].[SP_POSICION_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
                this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PosicionDTO>> GetPosicion(PosicionDTO request)
        {
            ResultDTO<PosicionDTO> res = new ResultDTO<PosicionDTO>();
            PosicionDTO item = new PosicionDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_posicion", request.iid_posicion);
                using (var cn = new SqlConnection(_connectionString))
                {
                    var query = await cn.QueryAsync<PosicionDTO>("[dbo].[SP_POSICION_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PosicionDTO)query.FirstOrDefault();
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
                this.iLogErrorRepository.RegisterLogError(lg);
            }
            return res;
        }

        public async Task<ResultDTO<PosicionDTO>> RegisterPosicion(PosicionDTO request)
        {
            ResultDTO<PosicionDTO> res = new ResultDTO<PosicionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_posicion", request.iid_posicion);
                        parameters.Add("@p_iid_piso", request.iid_piso);
                        parameters.Add("@p_icoordenada_x", request.icoordenada_x);
                        parameters.Add("@p_icoordenada_y", request.icoordenada_y);
                        parameters.Add("@p_iid_tipo_posicion", request.iid_tipo_posicion);
                        parameters.Add("@p_iid_sector", request.iid_sector);
                        parameters.Add("@p_icapacidad", request.icapacidad);

                        parameters.Add("@p_videntificador", request.videntificador);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                        //  parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        // parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_POSICION_REGISTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    this.iLogErrorRepository.RegisterLogError(lg);
                }
            }
            return res;
        }
    }
}
