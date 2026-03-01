namespace ef2.Data;

using Microsoft.EntityFrameworkCore;
using ef2.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CarOrder> CarOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarOrder>()
            .HasKey(co => new { co.CarId, co.CustomerId });

        modelBuilder.Entity<Car>()
            .HasIndex(c => new { c.Make, c.Model })
            .IsUnique();

        modelBuilder.Entity<Car>().HasQueryFilter(c => !c.IsDeleted);
    }
}

