using MasterClass.Repository.Models.Users;

namespace MasterClass.Business.Abstractions.Users
{
     public interface IUserBusiness
    {
        User AuthenticateUser(string login, string password);
    }
}