using System.Collections.Generic;

namespace Models.Restaurants.Models.Data
{
    public class RestaurantPlan
    {
        public int Id { get; set; }
        public string WebSvg { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
        public ICollection<PlanWall> Walls { get; set; }
        public ICollection<PlanTable> Tables { get; set; }
    }
}