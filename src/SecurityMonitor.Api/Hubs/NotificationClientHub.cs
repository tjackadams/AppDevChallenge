using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using SecurityMonitor.Core.Models.Devices;
using SecurityMonitor.Core.Models.Devices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Api.Hubs
{
    public class NotificationClientHub : Hub
    {
    }

    public class NotificationDispatcher : INotificationHandler<AlarmCreatedEvent>, INotificationHandler<DeviceUpdatedEvent>
    {
        private readonly IHubContext<NotificationClientHub> _hubContext;

        public NotificationDispatcher(IHubContext<NotificationClientHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(AlarmCreatedEvent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("newAlarm", notification, cancellationToken);
        }

        public Task Handle(DeviceUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("updatedDevice", notification, cancellationToken);
        }
    }
}
