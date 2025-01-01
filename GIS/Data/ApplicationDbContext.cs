using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace GIS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Elektrik direkleri tablosu
        public DbSet<ElectricPole> ElectricPoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PostGIS geometry türü tanımlaması
            modelBuilder.Entity<ElectricPole>()
                .Property(e => e.Location)
                .HasColumnType("geometry(Point, 4326)");
        }
    }

    public class ElectricPole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Point Location { get; set; } // PostGIS Point türü
    }
}
