using MediatR;
using Microsoft.AspNetCore.SignalR;
using SecurityMonitor.Core.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Api.Hubs
{
    public class AlarmClientHub : Hub
    {
    }

    public class AlarmClientDispatcher : INotificationHandler<AlarmCreatedEvent>
    {
        private readonly IHubContext<AlarmClientHub> _hubContext;

        public AlarmClientDispatcher(IHubContext<AlarmClientHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(AlarmCreatedEvent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("newAlarm", notification, cancellationToken);
        }
    }
}
