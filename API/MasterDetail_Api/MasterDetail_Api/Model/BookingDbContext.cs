using Microsoft.EntityFrameworkCore;

namespace MasterDetail_Api.Model;
public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Spot> Spots { get; set; }
    public DbSet<BookingEntry> BookingEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Spot>().HasData
            (
                new Spot { SpotId = 1, SpotName = "Panam City" },
                new Spot { SpotId = 2, SpotName = "Sylhet" },
                new Spot { SpotId = 3, SpotName = "Bandarban" },
                new Spot { SpotId = 4, SpotName = "Cox's Bazar" },
                new Spot { SpotId = 5, SpotName = "Bagerhat" }
            );
    }
}