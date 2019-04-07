using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core
{
    public class DeviceEvent
    {
        private DeviceEvent(){}

        public static DeviceEvent New(Guid id, DateTime eventTime, Status status)
        {
            return new DeviceEvent
            {
                Id = id,
                EventTime = eventTime,
                EventStatus = status
            };
        }

        public Guid Id { get; set; }
        public DateTime EventTime { get; set; }

        public Status EventStatus { get; set; }
    }
}
