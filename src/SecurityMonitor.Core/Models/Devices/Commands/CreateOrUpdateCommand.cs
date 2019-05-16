using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models.Devices
{
    public class CreateOrUpdateCommand
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
            private readonly IDeviceRepository _deviceRepository;
            public Handler(IDeviceRepository deviceRepository)
            {
                _deviceRepository = deviceRepository;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _deviceRepository.AddOrUpdateAsync(
                    request.Id,
                    request.Name,
                    request.Latitude,
                    request.Longitude);
                
                return Unit.Value;
            }
        }
    }


}
