using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BCDemo.Services;
using BCDemo.Models;
using System.IO;

namespace BCDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<ITransactionService, TransactionService>();

            //add a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            //add session
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(20);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseDefaultFiles();
            app.UseMvcWithDefaultRoute();

            //app.Use(async (context, next) => {
            //    await next();
            //    if (context.Response.StatusCode == 404 &&
            //       !Path.HasExtension(context.Request.Path.Value) &&
            //       !context.Request.Path.Value.StartsWith("/api/"))
            //    {
            //        context.Request.Path = "/index.html";
            //        await next();
            //    }
            //});
            //app.UseMvcWithDefaultRoute();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
        }
    }
}
