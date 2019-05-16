using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models.Devices
{
    public class CreateAlarmCommand
    {
        public class Request : IRequest
        {
            public int DeviceId { get; set; }
            public Guid Id { get; set; }
            public DateTimeOffset EventTime { get; set; }
            public Status Status { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IDeviceEventRepository _eventRepository;
            private readonly IMediator _mediator;
            public Handler(IDeviceEventRepository eventRepository, IMediator mediator)
            {
                _eventRepository = eventRepository;
                _mediator = mediator;
            }
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _eventRepository.AddAsync(
                    request.DeviceId,
                    request.Id,
                    request.EventTime,
                    request.Status);

                await _mediator.Publish(new AlarmCreatedEvent
                {
                    DeviceId = request.DeviceId,
                    EventTime = request.EventTime,
                    Id = request.Id,
                    Status = request.Status
                });

                return Unit.Value;
            }
        }
    }
}
