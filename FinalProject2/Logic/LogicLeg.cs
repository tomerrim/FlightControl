using FinalProject.Client.Hubs;
using FinalProject.DataAccess.Repositories;
using FinalProject.Models;

namespace FinalProject2.Server.Logic
{
    public class LogicLeg
    {
        private readonly IRepository _repository;
        private readonly FlightsHub _hub;
        public LogicLeg(IRepository repository, FlightsHub hub)
        {
            _repository = repository;
            _hub = hub;
        }
        public int FlightCounter { get; set; }

        public async Task AddFlight(Flight flight)
        {
            var startLeg = await _repository.FirstLegAsync();
            startLeg.Flight = flight;
            await NextTerminal(flight, startLeg);
        }

        private async Task NextTerminal(Flight flight, Leg leg)
        {
            var log = new Logger { Flight = flight, Leg = leg, In = DateTime.Now };
            await _repository.AddLoggerAsync(log);
            if (leg.Number == 1)
                Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"{flight.Id} - {flight.Brand} Leg: {leg.Number} ({DateTime.Now})");
            Console.ResetColor();

            if (leg.Number == 4)
            {
                if (!leg.IsChangeStatus)
                {
                    FlightCounter++;
                    if (FlightCounter == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        await Console.Out.WriteLineAsync($"terminals are at full capacity with {FlightCounter} flights , wait a few seconds...");
                        Console.ResetColor();
                    }
                }
                else
                {
                    FlightCounter--;
                }
            }

            Thread.Sleep(leg.WaitTime * 1000);
            var nextLeg = await _repository.NextLegAsync(leg);
            if (leg.CurrentLeg == LegNumber.Fou && flight.IsDeparture)
            {
                log.Out = DateTime.Now;
                leg.Flight = null;
                Console.ForegroundColor = ConsoleColor.Green;
                await Console.Out.WriteLineAsync($"{flight.Id} - {flight.Brand}: Departure from {leg.Number}");
                Console.ResetColor();
                await _repository.SaveAsync();
                return;
            }
            else if (nextLeg?.Flight == null)
            {
                nextLeg!.Flight = flight;
                leg.Flight = null;
                log.Out = DateTime.Now;
                await _hub.UpdateLoggers(log);

                flight.IsDeparture = leg.IsChangeStatus;
            }
            else if (nextLeg?.Flight != null)
            {
                await NextTerminal(flight, leg);
            }
            await _repository.SaveAsync();
            await NextTerminal(flight, nextLeg);
        }
    }
}
