using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityMonitor.Core.Device
{
    public class Device : IEntity
    {
        private Device(){}

        public static Device New(int id, string name, double lat, double lng)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name), "Device name must be supplied.");

            return new Device
            {
                Id = id,
                Name = name,
                Position = new Position
                {
                    Latitude = lat,
                    Longitude = lng
                }
            };
        }

        public void NewEvent(string imageUrl, EventRaised eventRaised)
        {
            ImageUrl = imageUrl;

            var deviceEvent = _deviceEvents.FirstOrDefault(de => de.Id == eventRaised.Id);
            if (deviceEvent != null)
            {
                return;
            }

            _deviceEvents.Add(DeviceEvent.New(eventRaised.Id, eventRaised.EventTime, eventRaised.Status));
        }

        public int Id { get; private set;}

        
        public string Name { get; private set; }
        public Position Position { get; private set; }
        public string ImageUrl { get; private set; }
        private readonly HashSet<DeviceEvent> _deviceEvents = new HashSet<DeviceEvent>();
        public IEnumerable<DeviceEvent> DeviceEvents => _deviceEvents.ToList();
    }

    public class EventRaised
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime EventTime { get; set; }
        public string Subject { get; set; }
    }

}
