using MediatR;
using SecurityMonitor.Core.Models.Devices.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models.Devices
{
    public class CreateOrUpdateDeviceCommand
    {
        public class Request : IRequest
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IMediator _mediator;
            private readonly IDeviceRepository _deviceRepository;
            public Handler(IMediator mediator, IDeviceRepository deviceRepository)
            {
                _mediator = mediator;
                _deviceRepository = deviceRepository;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _deviceRepository.AddOrUpdateAsync(
                    request.Id,
                    request.Name,
                    request.Latitude,
                    request.Longitude);

                await _mediator.Publish(new DeviceUpdatedEvent
                {
                    DeviceId = request.Id,
                    Name = request.Name,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                });
                
                return Unit.Value;
            }
        }
    }


}
