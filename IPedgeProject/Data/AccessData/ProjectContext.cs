using Microsoft.EntityFrameworkCore;

namespace IPedgeProject.Data.AccessData
{
  public class ProjectContext : DbContext
  {
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    { }
    public DbSet<Employee> Employee { get; set; }
  }
}
