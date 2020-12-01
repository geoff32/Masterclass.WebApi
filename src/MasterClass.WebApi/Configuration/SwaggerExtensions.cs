using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MasterClass.WebApi.Configuration
{
    public static class SwaggerExtensions
    {
        private const string VERSION = "v1";
        private const string SWAGGER_DOCNAME = "masterclass.webapi." + VERSION;

        public static IServiceCollection AddMasterClassSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(genOptions
                => genOptions.SwaggerDoc(SWAGGER_DOCNAME,
                    new OpenApiInfo
                    {
                        Title = "MasterClass WebApi",
                        Version = VERSION
                    }));

            return services;
        }

        public static IApplicationBuilder UseMasterClassSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(uiOptions => uiOptions.SwaggerEndpoint($"/swagger/{SWAGGER_DOCNAME}/swagger.json", "MasterClass WebApi"));

            return app;
        }
    }
}