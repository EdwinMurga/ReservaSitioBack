using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ReservaSitio.Entities.Enum;

namespace ReservaSitio.Repository.Log_Error
{
    public class LogErrorTablaRepository : BaseRepository, ILogErrorTablaRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;
        public LogErrorTablaRepository(ICustomConnection connection, IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
        public async Task<ResultDTO<LogErrorTablaDTO>> RegisterLogTablaError(LogErrorTablaDTO request)
        {
            ResultDTO<LogErrorTablaDTO> res = new ResultDTO<LogErrorTablaDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_log_tabla", request.iid_log_tabla);
                        parameters.Add("@p_vnombretabla", request.vnombretabla);
                        parameters.Add("@p_vaccion", request.vaccion);
                  
                     
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_LOG_TABLA_INSERTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
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
                    res.InnerException = "Log => " + e.Message.ToString();
                }
            }
            return res;
        }
    }
}
