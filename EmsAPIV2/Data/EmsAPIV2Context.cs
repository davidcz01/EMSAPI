using EmsAPIV2.Models;
using Microsoft.EntityFrameworkCore;

namespace EmsAPIV2.Data
{
    public class EmsAPIV2Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public EmsAPIV2Context() { }
        public EmsAPIV2Context(DbContextOptions<EmsAPIV2Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=EmsAPIV2;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
