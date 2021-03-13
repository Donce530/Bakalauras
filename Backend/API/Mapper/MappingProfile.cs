using System.IO.Compression;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


            CreateMap<WallDto, PlanWall>().ReverseMap();
            CreateMap<TableDto, PlanTable>().ReverseMap();

            CreateMap<RestaurantPlanDto, RestaurantPlan>().ReverseMap();
            CreateMap<RestaurantPlan, RestaurantPlan>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.RestaurantId, opt => opt.Ignore());

            CreateMap<PlanWall, PlanWall>()
                .EqualityComparison((src, dst) => src.Id.Equals(dst.Id));
            CreateMap<PlanTable, PlanTable>()
                .EqualityComparison((src, dst) => src.Id.Equals(dst.Id));
        }
    }
}
