using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core.Models.Devices.Events
{
    public class DeviceUpdatedEvent : INotification
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
