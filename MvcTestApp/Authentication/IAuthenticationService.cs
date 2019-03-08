using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MvcTestApp.Authentication
{
    public interface IAuthenticationService
    {
        Task<ClaimsPrincipal> Login(string userName, string password);
    }
}
