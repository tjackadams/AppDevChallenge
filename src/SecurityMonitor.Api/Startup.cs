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
using System;

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
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder.AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .SetIsOriginAllowed((host) => true)
                                   .AllowCredentials();
                        }));

            services
                .AddAutoMapper(typeof(Startup), typeof(Device));

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddDistributedMemoryCache();

            var container = new Container(cfg =>
            {
                cfg.For<ISimulator>(Lifecycles.Singleton).Use<Simulator.Simulator>();
                cfg.For<IDeviceRepository>().Use<DeviceRepository>();
                cfg.For<IDeviceEventRepository>().Use<DeviceEventRepository>();
            });

            services.AddMediatR(typeof(Startup), typeof(Device));

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });

            container.Populate(services);

            // TODO: Needs to be a background job
            var simulator = container.GetInstance<ISimulator>();

            simulator.Simulate();

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app
                .UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationClientHub>("/hub/notifications");
            });
        }
    }
}
