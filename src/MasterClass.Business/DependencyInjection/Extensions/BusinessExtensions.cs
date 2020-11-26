using MasterClass.Business.Abstractions.Users;
using MasterClass.Business.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MasterClass.Business.DependencyInjection.Extensions
{
    public static class BusinessExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IUserBusiness, UserBusiness>();

            return services;
        }
    } 
}