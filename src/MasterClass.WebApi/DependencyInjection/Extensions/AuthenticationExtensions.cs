using System;
using MasterClass.Core.Options;
using MasterClass.Core.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MasterClass.WebApi.DependencyInjection.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddMasterClassJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfigSection = config.GetSection(nameof(JwtOptions));
            services.Configure<JwtOptions>(jwtConfigSection);

            var jwtOptions = jwtConfigSection.Get<JwtOptions>();

            if (jwtOptions?.Enabled ?? false)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOptions.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.Key.ToBytes()),
                            AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                            ClockSkew = TimeSpan.FromSeconds(0)
                        };
                    });
            }

            return services;
        }
    }
}