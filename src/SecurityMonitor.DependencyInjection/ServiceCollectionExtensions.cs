using Microsoft.Extensions.DependencyInjection;
using SecurityMonitor.Core.Models;
using SecurityMonitor.Data.Repository;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureDependencyInjection(this IServiceCollection services)
        {
            var container = new Container(cfg =>
            {
                cfg.For<IAlarmRepository>().Use<AlarmRepository>();
                cfg.For<IDeviceRepository>().Use<DeviceRepository>();
                cfg.For<IDeviceEventRepository>().Use<DeviceEventRepository>();
            });

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }
    }
}
