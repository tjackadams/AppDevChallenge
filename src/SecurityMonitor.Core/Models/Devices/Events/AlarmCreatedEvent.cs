using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core.Models.Devices
{
    public class AlarmCreatedEvent : INotification
    {
        public int DeviceId { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset EventTime { get; set; }
        public Status Status { get; set; }
    }
}
