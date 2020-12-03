using System.Security.Claims;
using MasterClass.Service.Abstractions.Models.Users;
using MasterClass.Service.Models.Users;

namespace MasterClass.Service.Abstractions.Users
{
    public interface IUserService
    {
        IAuthenticatedUser Authenticate(AuthenticateParameters authParams);
        ClaimsPrincipal SignIn(AuthenticateParameters authParams, string scheme);
    }
}