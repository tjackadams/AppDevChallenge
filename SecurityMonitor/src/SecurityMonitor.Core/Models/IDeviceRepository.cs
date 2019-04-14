using SecurityMonitor.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models
{
    public interface IDeviceRepository : ITransientDependency
    {
        Task<IEnumerable<Device>> GetAll();
    }
}
