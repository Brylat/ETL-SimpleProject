using System;
using Microsoft.AspNetCore.SignalR;

namespace Etl.Logger {
    public class LoggerHub : Hub {
        public void Log (string message) {
            Clients.All.SendAsync ("sendToAll", message);
        }
    }
}