using FinalProject.Client.Hubs;
using FinalProject.DataAccess.Repositories;
using FinalProject.Models;
using FinalProject2.Server.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;

namespace FinalProject2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly LogicLeg _logic;
        private readonly IRepository _repository;
        private readonly FlightsHub _hub;
        //private readonly IHubContext<FlightsController> _hubContext;

        public FlightsController(IRepository repository, FlightsHub hub)
        {
            _repository = repository;
            _hub = hub;
            _logic = new LogicLeg(_repository,_hub);
            //_logic = logic;
        }

        //For Simulator only
        [HttpPost]
        public async Task AddFlightToSimulator(Flight flight)
        {
            try
            {
                await _repository.AddFlightAsync(flight);
                await _repository.SaveAsync();
                //if(_repository.SaveAsync() == 1)
                await _logic.AddFlight(flight);
                await _hub.Clients.All.SendAsync("SendNewFlightToClient", flight);
            }
            catch (Exception ex)
            {
                // change later
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        //For UI only
        //[HttpGet]
        ////public async Task<IEnumerable<Logger>> Get() => await _logic.AsyncList();
        //public async Task<IEnumerable<Logger>> Get()
        //{
        //    return await _repository.LoggerListAsync();
        //}

        //For UI only
        [HttpGet]
        public async Task<IEnumerable<Logger>> Get()
        {
            // Call the method in the SignalR hub to send all the logs to the clients
            var loggerList = await _repository.LoggerListAsync();
            //await _hub.UpdateLoggers();
            return loggerList;
            
            //return await _repository.LoggerListAsync();
        }
    }
}
