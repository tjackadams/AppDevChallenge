using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core.Models.Devices
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Status Status { get; set; }
    }
}
