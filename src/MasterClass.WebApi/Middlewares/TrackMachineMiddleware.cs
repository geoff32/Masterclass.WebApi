using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MasterClass.WebApi.Middlewares
{
    public class TrackMachineMiddleware
    {
        private readonly RequestDelegate _next;

        public TrackMachineMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Machine", Environment.MachineName);
            await _next(context);
        }
    }
}