using System;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurants.Models.Enums;

namespace Restaurants.Models.Data
{
    public class OpenHours
    {
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
        public WeekDay WeekDay { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}