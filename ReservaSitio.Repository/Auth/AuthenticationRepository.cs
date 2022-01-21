using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ReservaSitio.Abstraction.IRepository.Auth;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Auth;
using ReservaSitio.Repository.Base;

namespace ReservaSitio.Repository.Auth
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        private AuthenticationResponse pp = new AuthenticationResponse();
        private static HttpClient client = new HttpClient();

        public AuthenticationRepository(ICustomConnection connection) : base(connection)
        {
        }
        private async Task<ResultDTO<AuthenticationResponse>> InsertLogLogeo(AuthenticationResponse request)
        {
            ResultDTO<AuthenticationResponse> res = new ResultDTO<AuthenticationResponse>();
            try {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_usuario", request.Id);
                using (var scope = await mConnection.BeginConnection(true))
                {
                    using (var lector = await scope.ExecuteReaderAsync("[dbo].[PCLS_USUARIO_LOG_INSERT]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {

                            res.Informacion = Convert.ToString(lector["idusuario_acceso"].ToString()); //lector.GetValue(0).ToString() == "" ? "0" : lector.GetString(lector.GetOrdinal("idusuario_acceso"));
                            res.IsSuccess = true;
                        }
                    }
                    await mConnection.Complete();

                }
            }
            catch (Exception ex) 
            {
                res.InnerException = ex.Message.ToString();
                res.IsSuccess = false;
            }
            return res;
        }

    }
}
