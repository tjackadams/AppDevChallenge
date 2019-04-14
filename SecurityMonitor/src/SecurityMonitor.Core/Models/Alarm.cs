using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core.Models
{
    public class Alarm
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Text { get; set; }
        public Status Status { get; set; }
        public string ImageUrl { get; set; }

    }
}
