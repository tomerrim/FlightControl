using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly Dal _data;
        public Repository(Dal data)
        {
            _data = data;
        }

        async Task IRepository.AddFlightAsync(Flight flight) => await _data.AddAsync(flight);

        async Task IRepository.AddLoggerAsync(Logger logger) => await _data.Loggers.AddAsync(logger);

        async Task<Leg> IRepository.FirstLegAsync() => await _data.Legs.FirstAsync(l => l.Number == 1);

        async Task<IEnumerable<Logger>> IRepository.LoggerListAsync() => await _data.Loggers.Include(x => x.Leg).Include(x => x.Flight).ToListAsync();

        async Task<Leg> IRepository.NextLegAsync(Leg leg) => await _data.Legs.FirstOrDefaultAsync(l => leg.NextLegs.HasFlag(l.CurrentLeg));

        async Task IRepository.SaveAsync() => await _data.SaveChangesAsync();
    }
}
