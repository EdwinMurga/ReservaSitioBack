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
    public class PisoRepository : BaseRepository,IPisoRepository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        private readonly ILogErrorRepository iLogErrorRepository;
        public PisoRepository(ICustomConnection connection,
            IConfiguration configuration
            , ILogErrorRepository ILogErrorRepository) : base(connection)
        {
            this.iLogErrorRepository = ILogErrorRepository;
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");

        }
            public async Task<ResultDTO<PisoDTO>> DeletePiso(PisoDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDTO<PisoDTO>> GetListPiso(PisoDTO request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            List<PisoDTO> list = new List<PisoDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_piso", request.iid_piso);
                parameters.Add("@p_iid_local", request.iid_local);
                parameters.Add("@p_vdescipcion", request.vdescipcion);
                parameters.Add("@p_iid_nivel", request.iid_nivel);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);

                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_dfecha_registra_ini", request.dfecha_registra);
                parameters.Add("@p_dfecha_registra_fin", request.dfecha_registra);

                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<PisoDTO>)cn.Query<PisoDTO>("[dbo].[SP_PISOS_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<PisoDTO>> GetPiso(PisoDTO request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            PisoDTO item = new PisoDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_piso", request.iid_piso);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<PisoDTO>("[dbo].[SP_PISOS_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PisoDTO)query.FirstOrDefault();
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

        public async Task<ResultDTO<PisoDTO>> RegisterPiso(PisoDTO request)
        {
            ResultDTO<PisoDTO> res = new ResultDTO<PisoDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_piso", request.iid_piso);
                        parameters.Add("@p_iid_local", request.iid_local);
                        parameters.Add("@p_vdescipcion", request.vdescipcion);
                        parameters.Add("@p_iid_nivel", request.iid_nivel);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
         


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PISOS_REGISTRAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
