﻿using DemoApp.ApplicationCore.MessagingContracts;
using DemoApp.ApplicationCore.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.ClientNotifiers
{
    public sealed class NotificationGateway : INotifier
    {
        private readonly IHubContext<SignalRNotificationHub> _hubContext;

        public NotificationGateway(IHubContext<SignalRNotificationHub> hubContext)
            => _hubContext = hubContext;
        
        public async Task NotifyClientsAsync(Notification notification)
            => await _hubContext.Clients.All.SendAsync(notification.ToString());
    }
}
