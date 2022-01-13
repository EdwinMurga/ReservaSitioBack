using System.Threading.Tasks;
using ReservaSitio.DTOs.Auth;

namespace ReservaSitio.Abstraction.IService.Auth
{
    public interface IAuthenticationService
    {
      
        bool CheckPasswordAsync(string existingUserPasswordHash, string userPassword);
    }
}