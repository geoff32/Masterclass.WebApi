using MasterClass.Repository.Models.Users;

namespace MasterClass.Repository.Abstractions.Users
{
    public interface IUserRepository
    {
        User GetUser(string login);
    }
}