using MasterClass.Repository.Abstractions.Users;
using MasterClass.Repository.Models.Users;
using Microsoft.Extensions.Options;
using System.Linq;

namespace MasterClass.Repository.Mock.Users
{
    public class MockUserRepository : IUserRepository
    {
        private readonly MockUsers _mock;

        public MockUserRepository(IOptions<MockUsers> mock) => _mock = mock.Value;

        public User GetUser(string login) => _mock.Users.SingleOrDefault(user => user.Login == login);
    }
}