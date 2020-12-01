using MasterClass.Business.DependencyInjection.Extensions;
using MasterClass.Core.Options;
using MasterClass.Repository.DependencyInjection.Extensions;
using MasterClass.WebApi.Configuration;
using MasterClass.WebApi.Context;
using MasterClass.WebApi.DependencyInjection.Extensions;
using MasterClass.WebApi.Middlewares;
using MasterClass.WebApi.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MasterClass.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddMasterClassSwagger();
            services.Configure<DiagnosticOptions>(Configuration.GetSection("Diagnostic"));
            services.AddScoped<IApplicationRequestContext, ApplicationRequestContext>();
            
            services.ConfigureMock(Configuration);

            services.AddMasterClassAuthentication(Configuration);

            services.AddRepository();
            services.AddBusiness();
            services.AddService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<TrackMachineMiddleware>();
            app.UseHttpsRedirection();

            app.UseMasterClassSwaggerUI();

            app.UseRouting();
            app.UseMiddleware<TrackRequestContextMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
