using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DataAccess.Repositories
{
    public interface IRepository
    {
        Task AddFlightAsync(Flight flight);
        Task AddLoggerAsync(Logger logger);
        Task SaveAsync();
        Task<IEnumerable<Logger>> LoggerListAsync();
        Task<Leg> FirstLegAsync();
        Task<Leg> NextLegAsync(Leg leg);
    }
}
