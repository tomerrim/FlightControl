using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinalProject.DataAccess
{
    public class DalFactory : IDesignTimeDbContextFactory<Dal>
    {
        public Dal CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Dal>();
            optionsBuilder.UseSqlServer("Server=TOMERRIM;Database=MyAirport2;Trusted_Connection=True;TrustServerCertificate=True");
            return new Dal(optionsBuilder.Options);
        }
    }
}
