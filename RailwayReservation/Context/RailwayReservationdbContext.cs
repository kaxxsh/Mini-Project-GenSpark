using Microsoft.EntityFrameworkCore;
using RailwayReservation.Model.Domain;

namespace RailwayReservation.Context
{
    public class RailwayReservationdbContext : DbContext
    {
        public RailwayReservationdbContext(DbContextOptions<RailwayReservationdbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Model.Domain.Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the Train entity
            modelBuilder.Entity<Train>()
                .HasKey(t => t.TrainId);

            modelBuilder.Entity<Train>()
                .HasOne(t => t.TrainRoute)
                .WithOne()
                .HasForeignKey<Model.Domain.Route>(r => r.TrainId);

            // Configuring the Station entity
            modelBuilder.Entity<Station>()
                .HasKey(s => s.StationId);

            // Configuring the Route entity
            modelBuilder.Entity<Model.Domain.Route>()
                .HasKey(r => r.RouteId);

            modelBuilder.Entity<Model.Domain.Route>()
                .HasOne(r => r.SourceStation)
                .WithMany()
                .HasForeignKey(r => r.Source)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Model.Domain.Route>()
                .HasOne(r => r.DestinationStation)
                .WithMany()
                .HasForeignKey(r => r.Destination)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Model.Domain.Route>()
                .HasMany(r => r.Stations)
                .WithMany()
                .UsingEntity(j => j.ToTable("RouteStations"));
        }
    }
}
