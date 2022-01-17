using Dapper;

using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ReservaSitio.Entities.Enum;

using ReservaSitio.Abstraction.IRepository.LogError;
using Microsoft.Extensions.Configuration;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.Repository.Base;

namespace ReservaSitio.Repository.Log_Error
{
    public class LogErrorRepository: BaseRepository, ILogErrorRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        public LogErrorRepository(ICustomConnection connection, IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }

/**/
        public async Task<ResultDTO<LogErrorDTO>> RegisterLogError(LogErrorDTO request)
        {
            ResultDTO<LogErrorDTO> res = new ResultDTO<LogErrorDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn=await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_error", request.iid_error);
                        parameters.Add("@p_iid_opcion", request.iid_opcion);
                        parameters.Add("@p_ierror_number", request.ierror_number);
                        parameters.Add("@p_vdescripcion", request.vdescripcion);
                        parameters.Add("@p_ierror_line", request.ierror_line);
                        parameters.Add("@p_iid_tipo_mensaje", request.iid_tipo_mensaje);
                        parameters.Add("@p_vcodigo_mensaje", request.vcodigo_mensaje);

                        parameters.Add("@p_vorigen", request.vorigen);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_LOG_ERROR_INSERTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    res.InnerException = "Log => "+  e.Message.ToString();
                }
            }
            return res;
        }

    }
}
