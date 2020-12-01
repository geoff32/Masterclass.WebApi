using System.Security.Claims;
using MasterClass.Service.Models.Users;

namespace MasterClass.Service.Abstractions.Users
{
    public interface IUserService
    {
        AuthenticatedUser Authenticate(AuthenticateParameters authParams);
        ClaimsPrincipal SignIn(AuthenticateParameters authParams, string scheme);
    }
}