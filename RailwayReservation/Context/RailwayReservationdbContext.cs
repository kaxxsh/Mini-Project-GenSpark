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
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

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

            modelBuilder.Entity<Train>()
                .HasMany(t => t.Seats)
                .WithOne(s => s.Train)
                .HasForeignKey(s => s.TrainId);

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

            // Configuring the Passenger entity
            modelBuilder.Entity<Passenger>()
                .HasKey(p => p.PassengerId);

            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Seat)
                .WithMany()
                .HasForeignKey(p => p.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring the Seat entity
            modelBuilder.Entity<Seat>()
                .HasKey(s => s.SeatId);

            // Configuring the Ticket entity
            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Passengers)
                .WithOne()
                .HasForeignKey(p => p.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Train)
                .WithMany()
                .HasForeignKey(t => t.TrainId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.SourceStation)
                .WithMany()
                .HasForeignKey(t => t.Source)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.DestinationStation)
                .WithMany()
                .HasForeignKey(t => t.Destination)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
