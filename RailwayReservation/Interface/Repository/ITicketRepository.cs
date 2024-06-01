using RailwayReservation.Model.Domain;

namespace RailwayReservation.Interface.Repository
{
    public interface ITicketRepository:IRepository<Guid,Ticket>
    {
    }
}
