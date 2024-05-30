using RailwayReservation.Model.Domain;

namespace RailwayReservation.Interface.Repository
{
    public interface IStationRepository:IRepository<Guid, Station>
    {
    }
}
