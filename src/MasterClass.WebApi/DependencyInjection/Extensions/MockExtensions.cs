using MasterClass.Repository.Mock.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MasterClass.WebApi.DependencyInjection.Extensions
{
    public static class MockExtensions
    {
        public static IHostBuilder UseMockFiles(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((builderContext, configBuilder) =>
            {
                configBuilder.AddJsonFile("Mock/users.json");
            });

            return hostBuilder;
        }

        public static IServiceCollection ConfigureMock(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MockUsers>(config.GetSection(nameof(MockUsers)));

            return services;
        }
    }
}