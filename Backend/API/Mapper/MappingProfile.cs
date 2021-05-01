using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<UserDao, UserDataRow>();
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
            CreateMap<PlanLabel, LabelDto>().ReverseMap();
            CreateMap<TableDto, PlanTable>();
            CreateMap<PlanTable, TableDto>().ForMember(dst => dst.LinkedTableNumbers,
                opt => opt.MapFrom(src => src.LinkedTables.Select(lt => lt.Number)));

            CreateMap<RestaurantPlanDto, RestaurantPlan>();

            CreateMap<RestaurantPlan, RestaurantPlanDto>();
            
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

            CreateMap<Reservation, ReservationDataRow>()
                .ForMember(dst => dst.User,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dst => dst.Start,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.Start))
                .ForMember(dst => dst.End,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.End))
                .ForMember(dst => dst.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
                .ForMember(dst => dst.RealStart,
                    opt => opt.MapFrom(src => src.RealStart != null ? new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.RealStart : null))
                .ForMember(dst => dst.RealEnd,
                    opt => opt.MapFrom(src => src.RealEnd != null ? new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.RealEnd : null));

            CreateMap<Reservation, ReservationDetails>()
                .ForMember(dst => dst.UserFullName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dst => dst.UserEmail,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dst => dst.UserPhoneNumber,
                    opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dst => dst.Start,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.Start))
                .ForMember(dst => dst.End,
                    opt => opt.MapFrom(src => new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.End))
                .ForMember(dst => dst.RealStart,
                    opt => opt.MapFrom(src => src.RealStart != null ? new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.RealStart : null))
                .ForMember(dst => dst.RealEnd,
                    opt => opt.MapFrom(src => src.RealEnd != null ? new DateTime(src.Day.Year, src.Day.Month, src.Day.Day) + src.RealEnd : null))
                .ForMember(dst => dst.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
                .ForMember(dst => dst.TableSeats, opt => opt.MapFrom(src => src.Table.Seats))
                .ForMember(dst => dst.TableId, opt => opt.MapFrom(src => src.Table.Id))
                .ForMember(dst => dst.LinkedTableDetails, opt => opt.Ignore());
        }
    }
}
