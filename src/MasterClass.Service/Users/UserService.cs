using MasterClass.Business.Abstractions.Users;
using MasterClass.Service.Abstractions.Users;
using MasterClass.Service.Models.Users;

namespace MasterClass.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserBusiness _userBusiness;

        public UserService(IUserBusiness userBusiness) => _userBusiness = userBusiness;

        public AuthenticatedUser Authenticate(AuthenticateParameters authParams)
        {
            return AuthenticatedUser.Create(_userBusiness.AuthenticateUser(authParams.Login, authParams.Password));
        }
    }
}