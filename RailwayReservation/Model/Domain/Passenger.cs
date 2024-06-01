namespace RailwayReservation.Model.Domain
{
    public class Passenger
    {
        public Guid PassengerId { get; set; }
        public Guid TicketId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid? SeatId { get; set; }

        public Seat Seat { get; set; }
    }
}