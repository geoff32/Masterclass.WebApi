using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using MasterClass.Core.Options;
using MasterClass.Core.Tools;
using MasterClass.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MasterClass.WebApi.DependencyInjection.Extensions
{
    public static class AuthenticationExtensions
    {
        private const string DEFAULT_AUTHENTICATE_SCHEME = "DefaultAuthenticate";

        public static IServiceCollection AddMasterClassAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var authenticationSchemes = services.AddMasterClassSchemes(config).ToList();

            if (authenticationSchemes.Count > 1)
            {
                services
                    .AddAuthentication(options => options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = DEFAULT_AUTHENTICATE_SCHEME)
                    .AddPolicyScheme(DEFAULT_AUTHENTICATE_SCHEME, DEFAULT_AUTHENTICATE_SCHEME,
                        options => options.ForwardDefaultSelector = context => GetAuthenticateScheme(context, authenticationSchemes));
            }

            return services;
        }

        private static string GetAuthenticateScheme(Microsoft.AspNetCore.Http.HttpContext context, IEnumerable<string> authenticationSchemes)
        {
            var scheme = AuthenticationHeaderValue.TryParse(context.Request.Headers["Authorization"], out var authHeader)
                ? GetSchemeName(authHeader.Scheme, authenticationSchemes) : null;

            return scheme ?? GetSchemeName(CookieAuthenticationDefaults.AuthenticationScheme, authenticationSchemes) ?? authenticationSchemes.LastOrDefault();
        }

        private static string GetSchemeName(string scheme, IEnumerable<string> authenticationSchemes)
        {
            return authenticationSchemes.FirstOrDefault(s => string.Compare(scheme, s, true) == 0);
        }

        private static IEnumerable<string> AddMasterClassSchemes(this IServiceCollection services, IConfiguration config)
        {
            var jwtScheme = services.AddMasterClassJwt(config);
            if (!string.IsNullOrEmpty(jwtScheme))
            {
                yield return jwtScheme;
            }

            var cookieScheme = services.AddMasterClassCookie(config);
            if (!string.IsNullOrEmpty(cookieScheme))
            {
                yield return cookieScheme;
            }
        }

        private static string AddMasterClassJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfigSection = config.GetSection(nameof(JwtOptions));
            services.Configure<JwtOptions>(jwtConfigSection);

            var jwtOptions = jwtConfigSection.Get<JwtOptions>();

            if (jwtOptions?.Enabled ?? false)
            {
                services
                    .AddAuthentication(options => options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme)
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

                    return JwtBearerDefaults.AuthenticationScheme;
            }

            return null;
        }

        private static string AddMasterClassCookie(this IServiceCollection services, IConfiguration config)
        {
            var cookieOptions = config.GetSection(nameof(CookieOptions)).Get<CookieOptions>();
            if (cookieOptions?.Enabled ?? false)
            {
                services
                    .AddAuthentication(options =>
                        options.DefaultAuthenticateScheme
                            = options.DefaultChallengeScheme
                            = options.DefaultSignInScheme
                            = options.DefaultSignOutScheme
                            = CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = cookieOptions.Name;
                        options.Cookie.Domain = cookieOptions.Issuer;
                        options.EventsType = typeof(WebApiCookieAuthenticationEvents);
                        options.ExpireTimeSpan = cookieOptions.Duration;
                    });

                services.AddScoped<WebApiCookieAuthenticationEvents>();

                return CookieAuthenticationDefaults.AuthenticationScheme;
            }

            return null;
        }
    }
}