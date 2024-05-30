using Microsoft.EntityFrameworkCore;
using RailwayReservation.Model.Domain;

namespace RailwayReservation.Context
{
    public class RailwayReservationdbContext: DbContext
    {
        public RailwayReservationdbContext(DbContextOptions<RailwayReservationdbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
    }
}
