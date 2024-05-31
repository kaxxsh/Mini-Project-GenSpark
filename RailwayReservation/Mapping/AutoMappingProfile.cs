using AutoMapper;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;
using RailwayReservation.Model.Dtos.Train;
using RailwayReservation.Model.Dtos.Train.Route;
using RailwayReservation.Model.Dtos.Train.Station;
using System.Linq;

namespace RailwayReservation.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<StationRequestDto, Station>().ReverseMap();
            CreateMap<Station, StationResponseDto>().ReverseMap();
            CreateMap<Station, StationDto>().ReverseMap();
            CreateMap<User, UserRequestDto>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();

            CreateMap<Model.Domain.Route, RouteResponseDto>()
                .ForMember(dest => dest.Stations, opt => opt.MapFrom(src => src.Stations));
            CreateMap<RouteRequestDto, Model.Domain.Route>()
                .ForMember(dest => dest.Stations, opt => opt.Ignore());

            CreateMap<Train, TrainResponseDto>()
                .ForMember(dest => dest.TrainRoute, opt => opt.MapFrom(src => src.TrainRoute));
            CreateMap<TrainRequestDto, Train>()
                .ForMember(dest => dest.TrainRoute, opt => opt.MapFrom(src => src.TrainRoute));
        }
    }
}
