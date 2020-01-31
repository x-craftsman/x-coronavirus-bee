using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Craftsman.Core.CustomizeException;
using Craftsman.Core.Dependency.Installers;
using Craftsman.Core.MvcFilter;
using Craftsman.Waiter.WebUI.CustomFilter;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Craftsman.Waiter.WebUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHangfire(configuration =>
            {
                //configuration.UseRedisStorage("192.168.100.20:6379", new RedisStorageOptions { Db = 10, Prefix = "{hangfire-data}" });
                configuration.UseSqlServerStorage("Data Source=192.168.100.125;Initial Catalog=Hangfire;User Id=sa;Password=sql2019;");
                //
            });
            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services
               .AddMvc(options => {
                   options.Filters.Add<ModelValidationFilter>();
                   options.EnableEndpointRouting = false;
                })
               .AddControllersAsServices() //获取Controllers控制权，实现属性注入
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Craftsman.Waiter API", Version = "v1" });
            });

            var installer = new CoreInstaller();
            return installer.Install(services, "Waiter", WebPortalType.WebApplication);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseErrorHandling();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Waiter API V1");
            });

            // Add hangfire dashboard.
            var dashboardOptions = new DashboardOptions
            {
                //IgnoreAntiforgeryToken = true,
                Authorization = new[] { new HangfireNoAuthorizationFilter() }
            };
            app.UseHangfireDashboard("/hangfire", dashboardOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
