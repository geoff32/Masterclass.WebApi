using MasterClass.Service.Abstractions.Users;
using MasterClass.Service.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MasterClass.WebApi.StartupExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    } 
}