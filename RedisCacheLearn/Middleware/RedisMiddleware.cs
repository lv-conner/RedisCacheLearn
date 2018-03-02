using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCacheLearn.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RedisMiddleware
    {
        private readonly RequestDelegate _next;

        public RedisMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IDistributedCache distributedCache)
        {
            string currentTime = await distributedCache.GetStringAsync("currentTime");
            if(null == currentTime)
            {
                currentTime = DateTime.Now.ToString();
                await distributedCache.SetStringAsync("currentTime", currentTime);
            }
            await httpContext.Response.WriteAsync(currentTime);
            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RedisMiddlewareExtensions
    {
        public static IApplicationBuilder UseRedisMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedisMiddleware>();
        }
    }
}
