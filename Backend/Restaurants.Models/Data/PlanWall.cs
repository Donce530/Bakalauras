﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Models.Data
{
    [Table("PlanWalls")]
    public class PlanWall : PlanItem
    {
        public RestaurantPlan Plan { get; set; }
        public int PlanId { get; set; }
    }
}