using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train;

namespace RailwayReservation.Interface.Service
{
    public interface ITrainService
    {
        public Task<List<TrainResponseDto>> GetAll();
        public Task<TrainResponseDto> GetById(Guid id);
        public Task<TrainResponseDto> Add(TrainRequestDto trainRequestDto);
        public Task<TrainResponseDto> Update(Guid id, TrainRequestDto trainRequestDto);
        public Task<TrainResponseDto> Delete(Guid id);
        public Task<List<TrainResponseDto>> GetTrain(String From,String To, DateTime date);
    }
}
