using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SecurityMonitor.Core.Models.Devices;
using System;
using System.Threading.Tasks;

namespace SecurityMonitor.Simulator
{
    public class Simulator : ISimulator
    {
        // Speed of event publishing, ms between each event
        private int _eventInterval { get; set; }
        private int _numberOfDevices { get; set; }

        private static Random _random = new Random(Guid.NewGuid().GetHashCode());

        private static Random _randomStatus = new Random(Guid.NewGuid().GetHashCode());

        private static Random _randomTimer = new Random(Guid.NewGuid().GetHashCode());

        private readonly IMediator _mediator;

        private GeneratedDevice[] _devices;

        public ILogger Logger { get; set; }

        public Simulator(ILogger<Simulator> logger,IMediator mediator, IConfiguration configuration)
        {
            if(logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Logger = logger;
            _mediator = mediator;

            // set properties
            _eventInterval = configuration.GetSection("Simulator").GetValue<int>("EventInterval");
            _numberOfDevices = configuration.GetSection("Simulator").GetValue<int>("NumberOfDevices");
        }

        public async Task Simulate()
        {
            GenerateDevices();

            while (true)
            {
                int deviceId = _random.Next(0, _devices.Length);
                var device = _devices[deviceId];

                try
                {              
                    await _mediator.Send(new CreateOrUpdateDeviceCommand.Request
                    {
                        Id = device.Id,
                        Latitude = device.Latitude,
                        Longitude = device.Longitude,
                        Name = device.Name
                    });

                    await _mediator.Send(new CreateAlarmCommand.Request
                    {
                        DeviceId = device.Id,
                        EventTime = DateTimeOffset.UtcNow,
                        Id = Guid.NewGuid(),
                        Status = GetEventStatus()
                    });
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error while generating events for device: {0}", device.Name);
                }

                await Task.Delay(_randomTimer.Next(0, _eventInterval));
            }
        }

        private static Core.Status GetEventStatus()
        {
            var random = _randomStatus.Next(0, 100);
            if (random > 50 && random < 75)
            {
                return Core.Status.Warning;
            }

            if (random > 75)
            {
                return Core.Status.Danger;
            }

            return Core.Status.Ok;
        }

        private void GenerateDevices()
        {
            _devices = new GeneratedDevice[_numberOfDevices];

            for (int i = 0; i < _devices.Length; i++)
            {
                _devices[i] = new GeneratedDevice
                {
                    Id = i,
                    Name = string.Format("Alarm{0}", i),
                    Latitude = LocationProvider.Instance.GetLatitude(),
                    Longitude = LocationProvider.Instance.GetLongitude()
                };
            }
        }

        public class GeneratedDevice
        {
            public int Id { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public string Name { get; set; }
        }
    }
}