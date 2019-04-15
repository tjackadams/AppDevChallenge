using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMonitor.Api.Models
{
    public class AlarmViewModel
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }

    }
}
