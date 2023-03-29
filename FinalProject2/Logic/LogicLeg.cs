using FinalProject.DataAccess;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace FinalProject2.Server.Logic
{
    public class LogicLeg
    {
        private readonly Dal _data;
        public LogicLeg(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<Dal>();
            optionsBuilder.UseSqlServer(connectionString);
            _data = new Dal(optionsBuilder.Options);
        }

        //public LogicLeg(Dal data)
        //{
        //    _data = data;
        //}
        public int FlightCounter { get; set; }

        //public async Task AddFlight()
        //{
        //    string[] brands = { "Arkia", "Brussels Airlines", "Emirates", "El-Al" }; // add later more brands
        //    var flight = new Flight { IsDeparture = false, Brand = GetRandomBrand(brands), PassengersCount = GetRandomPassengers() };
        //    var startLeg = await data.Legs.FirstAsync(l => l.Number == 1);
        //    startLeg.Flight = flight;
        //    await NextTerminal(flight, startLeg);
        //}

        public async Task AddFlight(Flight flight)
        {
            var startLeg = await _data.Legs.FirstAsync(l => l.Number == 1);
            startLeg.Flight = flight;
            await NextTerminal(flight, startLeg);
        }

        private async Task NextTerminal(Flight flight, Leg leg)
        {
            var log = new Logger { Flight = flight, Leg = leg, In = DateTime.Now };
            await _data.Loggers.AddAsync(log);
            if(leg.Number == 1)
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
                        // add logic to stop creating new flights and stop the flights that on terminal 1,2,3 until FlightCounter < 4
                    }
                }
                else
                {
                    FlightCounter--;
                }
            }

            Thread.Sleep(leg.WaitTime * 1000);
            var nextLeg = await _data.Legs.FirstOrDefaultAsync(l => leg.NextLegs.HasFlag(l.CurrentLeg));
            if (leg.CurrentLeg == LegNumber.Fou && flight.IsDeparture)
            {
                log.Out = DateTime.Now;
                leg.Flight = null;
                Console.ForegroundColor = ConsoleColor.Green;
                await Console.Out.WriteLineAsync($"{flight.Id} - {flight.Brand}: Departure from {leg.Number}");
                Console.ResetColor();
                await _data.SaveChangesAsync();
                return;
            }
            else if (nextLeg?.Flight == null)
            {
                nextLeg!.Flight = flight;
                leg.Flight = null;
                log.Out = DateTime.Now;

                flight.IsDeparture = leg.IsChangeStatus;
            }
            await SaveAsync();

            await NextTerminal(flight, nextLeg);
        }

        //move later to repository
        public async Task AsyncAdd(Flight flight) => await _data.AddAsync(flight);
        public async Task SaveAsync() => await _data.SaveChangesAsync();
        //public async Task<IEnumerable<Flight>> AsyncList() => await data.Flights.ToListAsync();
        public async Task<IEnumerable<Logger>> AsyncList() => await _data.Loggers.Include(x => x.Leg).Include(x => x.Flight).ToListAsync();

        //
    }
}
