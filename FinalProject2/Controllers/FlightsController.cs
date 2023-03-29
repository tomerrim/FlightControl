using FinalProject.Models;
using FinalProject2.Server.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinalProject2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly LogicLeg _logic;
        public FlightsController(IConfiguration config)
        {
            _logic = new LogicLeg(config);
        }

        //For Simulator only
        [HttpPost]
        public async Task AddFlightToSimulator(Flight flight)
        {
            //flight.Id = ;
            await _logic.AsyncAdd(flight);
            await _logic.AddFlight(flight);
            await _logic.SaveAsync();
        }

        ///For UI only
        //[HttpGet]
        //public async Task<IEnumerable<Flight>> Get() => await logic.AsyncList();

        ///For UI only
        [HttpGet]
        public async Task<IEnumerable<Logger>> Get() => await _logic.AsyncList();
    }
}
