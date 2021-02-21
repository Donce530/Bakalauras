using System;
using Restaurants.Models.Enums;

namespace Restaurants.Models.Dto
{
    public class OpenHoursDto
    {
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
        public WeekDay WeekDay { get; set; }
    }
}