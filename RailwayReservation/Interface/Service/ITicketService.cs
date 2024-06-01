using RailwayReservation.Model.Dtos.Ticket;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Interface.Service
{
    public interface ITicketService
    {
        public Task<List<TicketResponseDto>> GetAll();
        public Task<TicketResponseDto> GetById(Guid id);
        public Task<TicketResponseDto> Add(TicketRequestDto ticketRequestDto);
        public Task<TicketResponseDto> Update(Guid id, TicketRequestDto ticketRequestDto);
        public Task<TicketResponseDto> Delete(Guid id);
        public Task<TicketResponseDto> CancelTicket(Guid id, TicketStatus ticketStatus);
        public Task<List<TicketResponseDto>> PendingTicket();
        public Task<TicketResponseDto> ApproveTicket(Guid id, TicketStatus ticketStatus);
        public Task<List<TicketResponseDto>> BookedTicket(Guid TrainId);
        public Task<List<TicketResponseDto>> GetBookedTicketbyUser(Guid id);
        public Task<List<TicketResponseDto>> GetCanceledTicketByUser(Guid id);
    }
}
