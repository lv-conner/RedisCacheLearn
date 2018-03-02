using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheLearn.Middleware;

namespace RedisCacheLearn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(options=> 
            {
                options.Configuration = "localhost";
                options.InstanceName = "Demo";
            });
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMemoryCacheMiddleware();
            app.UseRedisMiddleware();
            //app.Run(async (context) =>
            //{
            //    IMemoryCache memoryCache = context.RequestServices.GetService<IMemoryCache>();
            //    DateTime currentTime;
            //    if (!memoryCache.TryGetValue<DateTime>("currentTime", out currentTime))
            //    {
            //        memoryCache.Set<DateTime>("currentTime", currentTime = DateTime.Now);
            //    }
            //    await context.Response.WriteAsync($"current time is \t{ currentTime}");
            //});
        }
    }
}
