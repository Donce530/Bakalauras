using System;
using Models.Restaurants.Models.Enums;

namespace Models.Restaurants.Models.Dto
{
    public class OpenHoursDto
    {
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
        public WeekDay WeekDay { get; set; }
    }
}