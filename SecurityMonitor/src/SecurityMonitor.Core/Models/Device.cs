using System;

namespace SecurityMonitor.Core.Models
{
    public class Device : IEntity
    {
        private Device() { }

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

            if (LatestEvent.Id != eventRaised.Id && eventRaised.EventTime > LatestEvent.EventTime)
            {
                LatestEvent = DeviceEvent.New(eventRaised.Id, eventRaised.EventTime, eventRaised.Status);
            }
        }

        public int Id { get; private set; }


        public string Name { get; private set; }
        public Position Position { get; private set; }
        public string ImageUrl { get; private set; }

        public DeviceEvent LatestEvent { get; private set; }
    }
}
