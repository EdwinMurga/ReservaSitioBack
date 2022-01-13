using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reactive.Subjects;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReservaSitio.Abstraction;

namespace ReservaSitio.Services
{
    public interface ITokenHandlerService
    {
        string GenerateJwtTokenLogin(ITokenParameters pars);
        ITokenParameters GetObjectTokenLogin(string token);
        string GenerateJwtTokenPasswordRecover(ITokenParameters pars);
        ITokenParameters GetObjectTokenPasswordRecover(string token);
    }
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly JwtConfig _jwtConfig;
        public TokenHandlerService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public string GenerateJwtTokenLogin(ITokenParameters pars)
        {
            //pars = new ITokenParameters();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key= Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    //new Claim("Id", pars.Id),
                    new Claim(JwtRegisteredClaimNames.NameId,pars.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, pars.PasswordHash),
                    new Claim(JwtRegisteredClaimNames.Email, pars.UserName)
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public ITokenParameters GetObjectTokenLogin(string token)
        {
            ITokenParameters pars = new ITokenParameters();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            pars.Id =  claims.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            pars.PasswordHash = claims.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            pars.UserName = claims.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            return pars;
        }

        public string GenerateJwtTokenPasswordRecover(ITokenParameters pars)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", pars.Id),
                    new Claim(JwtRegisteredClaimNames.Birthdate, pars.FechaCaduca.ToLongTimeString())
                }),

                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public ITokenParameters GetObjectTokenPasswordRecover(string token)
        {
            ITokenParameters pars = new ITokenParameters();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            pars.Id = claims.Claims.First(claim => claim.Type == "Id").Value;
            pars.FechaCaduca = Convert.ToDateTime(claims.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth").Value);
            return pars;
        }
    }
}
