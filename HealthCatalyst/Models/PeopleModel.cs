using Microsoft.EntityFrameworkCore;

namespace Manza.HealthCatalyst.Models
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sample.db");
        }
    }
}
