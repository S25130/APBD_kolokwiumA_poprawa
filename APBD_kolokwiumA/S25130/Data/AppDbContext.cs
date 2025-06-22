using Microsoft.EntityFrameworkCore;
using S25130.Models;
namespace S25130.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<SeedlingBatch> Batches { get; set; }
    public DbSet<TreeSpecies> Species { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Responsible>()
            .HasKey(r => new { r.BatchId, r.EmployeeId });

        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.Batch)
            .WithMany(b => b.Responsibles)
            .HasForeignKey(r => r.BatchId);

        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.Employee)
            .WithMany(e => e.Responsibles)
            .HasForeignKey(r => r.EmployeeId);

        modelBuilder.Entity<Nursery>().HasData(new List<Nursery>
        {
            new Nursery { NurseryId = 1, Name = "Green Forest Nursery", EstablishedDate = new DateTime(2005, 5, 10) }
        });

        modelBuilder.Entity<TreeSpecies>().HasData(new List<TreeSpecies>
        {
            new TreeSpecies { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 5 }
        });

        modelBuilder.Entity<SeedlingBatch>().HasData(new List<SeedlingBatch>
        {
            new SeedlingBatch
            {
                BatchId = 1,
                NurseryId = 1,
                SpeciesId = 1,
                Quantity = 500,
                SownDate = new DateTime(2024, 3, 15),
                ReadyDate = new DateTime(2029, 3, 15)
            }
        });

        modelBuilder.Entity<Employee>().HasData(new List<Employee>
        {
            new Employee { EmployeeId = 1, FirstName = "Anna", LastName = "Kowalska", HireDate = new DateTime(2020, 1, 1) },
            new Employee { EmployeeId = 2, FirstName = "Jan", LastName = "Nowak", HireDate = new DateTime(2021, 2, 15) }
        });

        modelBuilder.Entity<Responsible>().HasData(new List<Responsible>
        {
            new Responsible { BatchId = 1, EmployeeId = 1, Role = "Supervisor" },
            new Responsible { BatchId = 1, EmployeeId = 2, Role = "Planter" }
        });
    }
}
