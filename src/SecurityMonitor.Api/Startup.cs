using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityMonitor.Api.Hubs;
using SecurityMonitor.Core.Models;
using SecurityMonitor.Data.Repository;
using SecurityMonitor.Simulator;
using StructureMap;
using StructureMap.Pipeline;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors();

            services
                .AddAutoMapper(typeof(Startup));

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddDistributedMemoryCache();

            var container = new Container(cfg =>
            {
                cfg.For<ISimulator>(Lifecycles.Singleton).Use<Simulator.Simulator>();
                cfg.For<IDeviceRepository>().Use<DeviceRepository>();
                cfg.For<IDeviceEventRepository>().Use<DeviceEventRepository>();
            });

            services.AddMediatR(typeof(Startup), typeof(Device));

            services.AddSignalR();

            container.Populate(services);

            //services
            //    .AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "Security Monitor API", Version = "v1" });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});

            var simulator = container.GetInstance<ISimulator>();

            simulator.Simulate();

            return container.GetInstance<IServiceProvider>();
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

            //app
            //    .UseSwagger();


            //app
            //    .UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security Monitor API V1");
            //    c.RoutePrefix = string.Empty;
            //});

            app
                .UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<AlarmClientHub>("/alarms");
            });
        }
    }
}
