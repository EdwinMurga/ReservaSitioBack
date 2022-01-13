using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Entities.Autenticacion;

namespace ReservaSitio.Application.Autenticacion
{
    public interface IAutenticacion
    {
        Task<AUTENTICACION_OBJ> FindByEmail(string userEmail);
        Task<bool> CheckPasswordAsync(AUTENTICACION_OBJ existingUser, string userPassword);
    }

    public class autenticacionLogic : IAutenticacion
    {
        public IConfiguration _Configuration { get; }
        protected string _connectionString;

        public autenticacionLogic(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }
        public async Task<AUTENTICACION_OBJ> FindByEmail(string userEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@usuac_vnro_documento", userEmail);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<AUTENTICACION_OBJ>("[dbo].[PCLS_USUARIO_GET_AUTENTICACION]", parameters, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<bool> CheckPasswordAsync(AUTENTICACION_OBJ existingUser, string userPassword)
        {
            return  (existingUser.PasswordHash == userPassword);
        }
    }
}
