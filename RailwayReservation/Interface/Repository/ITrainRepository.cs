using RailwayReservation.Model.Domain;

namespace RailwayReservation.Interface.Repository
{
    public interface ITrainRepository:IRepository<Guid, Train>
    {
    }
}
