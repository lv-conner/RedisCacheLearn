using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace RedisCacheLearn.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MemoryCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public MemoryCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,IMemoryCache cache)
        {
            DateTime currentTime;
            if (!cache.TryGetValue<DateTime>("currentTime", out currentTime))
            {
                cache.Set<DateTime>("currentTime", currentTime = DateTime.Now);
            }
            await httpContext.Response.WriteAsync($"current time is \t{ currentTime}");
            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MemoryCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseMemoryCacheMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MemoryCacheMiddleware>();
        }
    }
}
