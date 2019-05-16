using SecurityMonitor.Core.Domain;
using System;

namespace SecurityMonitor.Core.Models
{
    public class DeviceEvent : IEntity<Guid>
    {
        private DeviceEvent(){}

        public static DeviceEvent New(int deviceId, Guid id, DateTimeOffset eventTime, Status status)
        {
            return new DeviceEvent
            {
                DeviceId = deviceId,
                Id = id,
                EventTime = eventTime,
                EventStatus = status
            };
        }

        public int DeviceId { get; private set; }
        public Guid Id { get; private set; }
        public DateTimeOffset EventTime { get; private set; }
        public Status EventStatus { get; private set; }
    }
}
