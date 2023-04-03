using FinalProject.DataAccess.Repositories;
using FinalProject.Models;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.Client.Hubs
{
    public class FlightsHub : Hub
    {
        private readonly IHubContext<FlightsHub> _hubContext;
        private readonly IRepository _repository;
        public FlightsHub(IHubContext<FlightsHub> hubContext, IRepository repository)
        {
            _hubContext = hubContext;
            _repository = repository;
        }

        public async Task SendNewFlightToClient(Flight flight)
        {
            Console.WriteLine("SendNewFlightToClient method called");
            await _hubContext.Clients.All.SendAsync("addNewRow", flight);
        }

        public async Task UpdateLoggers(Logger logger)
        {
            Console.WriteLine("UpdateLoggers method called");
            //var loggers = await _repository.LoggerListAsync();
            await _hubContext.Clients.All.SendAsync("refreshLoggers", logger);
        }

        //public override async Task OnConnectedAsync()
        //{
        //    Console.WriteLine("Client connected");
        //    await base.OnConnectedAsync();
        //    await UpdateLoggers();
        //}

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Client disconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
