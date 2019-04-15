using SecurityMonitor.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models
{
    public interface IAlarmRepository : ITransientDependency
    {
        Task<IEnumerable<Alarm>> GetAllLatest();
    }
}
