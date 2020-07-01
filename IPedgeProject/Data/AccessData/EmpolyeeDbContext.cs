using Microsoft.EntityFrameworkCore;

namespace IPedgeProject.Data.AccessData
{
    public class EmpolyeeDbContext:DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public EmpolyeeDbContext(DbContextOptions<EmpolyeeDbContext> options):base(options)
        {

        }
    }
}
