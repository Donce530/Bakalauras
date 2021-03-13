using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Models.Data
{
    [Table("PlanTables")]
    public class PlanTable : PlanItem
    {
        public int Seats { get; set; }
        public int Number { get; set; }

        public RestaurantPlan Plan { get; set; }
        public int PlanId { get; set; }
    }
}