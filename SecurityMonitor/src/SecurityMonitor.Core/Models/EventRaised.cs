using System;

namespace SecurityMonitor.Core.Models
{
    public class EventRaised
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime EventTime { get; set; }
        public string Subject { get; set; }
    }
}