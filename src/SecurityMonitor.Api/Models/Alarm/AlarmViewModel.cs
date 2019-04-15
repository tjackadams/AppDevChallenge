namespace SecurityMonitor.Api.Models
{
    public class AlarmViewModel
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }

    }
}
