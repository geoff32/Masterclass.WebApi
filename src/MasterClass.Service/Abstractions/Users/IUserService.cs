using MasterClass.Service.Models.Users;

namespace MasterClass.Service.Abstractions.Users
{
    public interface IUserService
    {
        AuthenticatedUser Authenticate(AuthenticateParameters authParams);
    }
}