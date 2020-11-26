using MasterClass.Repository.Abstractions.Users;
using MasterClass.Repository.Mock.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MasterClass.Repository.DependencyInjection.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, MockUserRepository>();

            return services;
        }
    } 
}