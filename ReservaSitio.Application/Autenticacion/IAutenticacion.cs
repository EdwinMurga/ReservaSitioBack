using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.Entities.Autenticacion;

namespace ReservaSitio.Application.Autenticacion
{
    public interface IAutenticacion
    {
        Task<ResultDTO<UsuarioDTO>> FindByEmail(string userEmail);
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
        public async Task<ResultDTO<UsuarioDTO>> FindByEmail(string userEmail)
        {

            ResultDTO<UsuarioDTO> res = new ResultDTO<UsuarioDTO>();
            UsuarioDTO item = new UsuarioDTO();
            var parameters = new DynamicParameters();
            //parameters.Add("@usuac_vnro_documento", userEmail);
            parameters.Add("@p_vnro_documento", "");
            parameters.Add("@p_vnombres", "");
            parameters.Add("@p_vapellido_paterno", "");
            parameters.Add("@p_vapellido_materno","");
            parameters.Add("@p_vcorreo_electronico", userEmail);
            using (var connection = new SqlConnection(_connectionString))
            {
               // return connection.Query<UsuarioDTO>("[dbo].[PCLS_USUARIO_GET_AUTENTICACION]", parameters, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                var query = await connection.QueryAsync<UsuarioDTO>("[dbo].[SP_USUARIO_BY_PARAMETER]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                item = (UsuarioDTO)query.FirstOrDefault();
                res.IsSuccess = (query.Any() == true ? true : false);
                res.item = (query.Any() == true ? item : null);
                return res;
            }
        }

        public async Task<bool> CheckPasswordAsync(AUTENTICACION_OBJ existingUser, string userPassword)
        {
            return  (existingUser.PasswordHash == userPassword);
        }
    }
}
