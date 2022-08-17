using Csi.Ems.Api.Core.Domain;
using Csi.Ems.Api.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Csi.Ems.Api.Persistence
{
    public class EmsDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EmployeeConfiguration().Configure(modelBuilder.Entity<Employee>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
