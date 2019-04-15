using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityMonitor.Core.DependencyInjection;
using SecurityMonitor.Core.Models;
using SecurityMonitor.Data.Repository;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SecurityMonitor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
    .AddCors();

            services
                .AddAutoMapper();

            services
                .Scan(scan =>
                {
                    scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                    .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                    .AsImplementedInterfaces()
                    .WithLifetime(ServiceLifetime.Transient);
                });

            services
                .AddTransient<IAlarmRepository, AlarmRepository>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Security Monitor API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            app
                .UseSwagger();


            app
                .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security Monitor API V1");
                c.RoutePrefix = string.Empty;
            });

            app
                .UseMvc();
        }
    }
}
