using System.Threading.Tasks;
using MasterClass.WebApi.Context;
using Microsoft.AspNetCore.Http;

namespace MasterClass.WebApi.Middlewares
{
    public class TrackRequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public TrackRequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApplicationRequestContext requestContext)
        {
            context.Response.Headers.Add("X-Guid", requestContext.Id.ToString());
            await _next(context);
        }
    }
}