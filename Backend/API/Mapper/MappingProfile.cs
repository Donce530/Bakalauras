using System;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Models.Reservations.Models.Data;
using Models.Reservations.Models.Dto;
using Models.Restaurants.Models.Data;
using Models.Restaurants.Models.Dto;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDao, User>();
            CreateMap<RegisterRequest, UserDao>();

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

            CreateMap<Restaurant, RestaurantPageItemDto>();

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

            CreateMap<NewReservationDto, Reservation>()
                .ForMember(dst => dst.Start, opt => opt.MapFrom(src => src.Start.TimeOfDay))
                .ForMember(dst => dst.End, opt => opt.MapFrom(src => src.End.TimeOfDay));

            CreateMap<Reservation, ReservationListItemDto>()
                .ForMember(dst => dst.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
                .ForMember(dst => dst.TableSeats, opt => opt.MapFrom(src => src.Table.Seats))
                .ForMember(dst => dst.RestaurantTitle, opt => opt.MapFrom(src => src.Restaurant.Title))
                .ForMember(dst => dst.RestaurantAddress, opt => opt.MapFrom(src => src.Restaurant.Address))
                .ForMember(dst => dst.Start,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.Start))
                .ForMember(dst => dst.End,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.End));
        }
    }
}
