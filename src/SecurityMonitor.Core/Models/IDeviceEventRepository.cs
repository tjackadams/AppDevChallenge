using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models
{
    public interface IDeviceEventRepository
    {
        Task AddAsync(int deviceId, Guid id, DateTimeOffset eventTime, Status status);
    }
}
