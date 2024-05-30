using AutoMapper;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;
using RailwayReservation.Model.Dtos.Train.Station;

namespace RailwayReservation.Mapping
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<StationRequestDto, Station>().ReverseMap();
            CreateMap<Station, StationResponseDto>().ReverseMap();
            CreateMap<Station, StationDto>().ReverseMap();
            CreateMap<User, UserRequestDto>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
        }
    }
}
