using Microsoft.EntityFrameworkCore;
using SignalR_Db_Listener.Database.Entities;

namespace SignalR_Db_Listener.Database
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
               optionsBuilder.UseSqlServer("Data Source=DESKTOP-EUDN6E9;Initial Catalog=signalR-db-listener;Trusted_Connection=True");

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasKey(x => x.Id);
        }
    }
}
