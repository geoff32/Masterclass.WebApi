using MasterClass.Repository.Models.Users;

namespace MasterClass.Service.Models.Users
{
    public class AuthenticatedUser
    {
        public int Id { get; }

        private AuthenticatedUser(int id)
        {
            Id = id;
        }

        internal static AuthenticatedUser Create(User user)
            => user == null ? null : new AuthenticatedUser(user.Id);
    }
}