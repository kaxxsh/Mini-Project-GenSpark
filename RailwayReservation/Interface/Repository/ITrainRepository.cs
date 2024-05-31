using RailwayReservation.Model.Domain;

namespace RailwayReservation.Interface.Repository
{
    public interface ITrainRepository:IRepository<Guid, Train>
    {
        Task<Seat> GetSeat(Guid seatId);
        Task AddSeats(IEnumerable<Seat> seats);
        Task RemoveSeats(IEnumerable<Seat> seats);
    }
}
