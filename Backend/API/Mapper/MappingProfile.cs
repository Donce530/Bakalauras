using AutoMapper;
using Restaurants.Models.Data;
using Restaurants.Models.Dto;
using Users.Models.Dao;
using Users.Models.Data;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDao, User>();

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dst => dst.Schedule,
                    opt => opt.MapFrom(src => src.Schedule));
            CreateMap<OpenHours, OpenHoursDto>();
            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(dst => dst.Schedule,
                    opt => opt.MapFrom(src => src.Schedule));
            CreateMap<OpenHoursDto, OpenHours>();

            CreateMap<Restaurant, Restaurant>()
                .ForMember(dst => dst.Id, opt => opt.Ignore());
            CreateMap<OpenHours, OpenHours>();
        }
    }
}
