using FinalProject.Models;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.Client.Hubs
{
    public class FlightsHub : Hub
    {
        private readonly IHubContext<FlightsHub> _hubContext;
        public FlightsHub(IHubContext<FlightsHub> hubContext) => _hubContext = hubContext;
        public async Task UpdateLoggers(Logger logger) => await _hubContext.Clients.All.SendAsync("refreshLoggers", logger);
    }
}
