using FinalProject.Client.Hubs;
using FinalProject.DataAccess.Repositories;
using FinalProject.Models;
using FinalProject2.Server.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly LogicLeg _logic;
        private readonly IRepository _repository;
        private readonly FlightsHub _hub;

        public FlightsController(IRepository repository, FlightsHub hub)
        {
            _repository = repository;
            _hub = hub;
            _logic = new LogicLeg(_repository, _hub);
        }

        //For Simulator only
        [HttpPost]
        public async Task AddFlightFromSimulator(Flight flight)
        {
            try
            {
                await _repository.AddFlightAsync(flight);
                await _repository.SaveAsync();
                await _logic.AddFlight(flight);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        //For UI only
        [HttpGet]
        public async Task<IEnumerable<Logger>> Get() => await _repository.LoggerListAsync();
    }
}
