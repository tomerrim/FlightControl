using FinalProject.Models;

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
