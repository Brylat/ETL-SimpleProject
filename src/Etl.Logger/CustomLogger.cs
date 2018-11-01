using Microsoft.AspNetCore.SignalR;

namespace Etl.Logger {
    public class CustomLogger : ICustomLogger {
        private readonly IHubContext<LoggerHub> _hubContext;

        public CustomLogger (IHubContext<LoggerHub> hubContext) {
            _hubContext = hubContext;
        }
        public void Log (string message) {
            _hubContext.Clients.All.SendAsync("sendToAll", message);
        }
    }
}