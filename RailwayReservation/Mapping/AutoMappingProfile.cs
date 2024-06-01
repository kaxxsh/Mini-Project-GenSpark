using AutoMapper;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;
using RailwayReservation.Model.Dtos.Train.Route;
using RailwayReservation.Model.Dtos.Train.Station;
using RailwayReservation.Model.Dtos.Train;
using RailwayReservation.Model.Dtos.Train.Seat;
using RailwayReservation.Model.Dtos.Ticket;
using RailwayReservation.Model.Dtos.Ticket.Passenger;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserRequestDto, User>();

        CreateMap<Station, StationResponseDto>().ReverseMap();
        CreateMap<StationRequestDto, Station>().ReverseMap();
        CreateMap<StationDto, Station>().ReverseMap();

        CreateMap<RailwayReservation.Model.Domain.Route, RouteResponseDto>();
        CreateMap<RouteRequestDto, RailwayReservation.Model.Domain.Route>();

        CreateMap<Train, TrainResponseDto>();
        CreateMap<TrainRequestDto, Train>();
        CreateMap<Seat, SeatResponseDto>(); 
        CreateMap<Train, TrainResponseDto>()
            .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats));

        CreateMap<TicketRequestDto, Ticket>().ReverseMap();
        CreateMap<Ticket, TicketResponseDto>().ReverseMap();
        CreateMap<PassengerRequestDto,Passenger>().ReverseMap();
        CreateMap<Passenger, PassengerResponseDto>().ReverseMap();
    }
}
