using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Craftsman.Core.Dependency.Installers;
using Microsoft.Extensions.Hosting;

namespace Craftsman.LaunchPad.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddControllersAsServices() //获取Controllers控制权，实现属性注入
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var installer = new CoreInstaller();
            var serviceProvider = installer.Install(services, "LaunchPad");
            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
