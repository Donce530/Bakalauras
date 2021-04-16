using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Restaurants.Models.Data
{
    [Table("PlanLabels")]
    public class PlanLabel : PlanItem
    {
        public string Text { get; set; }
        public double FontSize { get; set; }
        
        public RestaurantPlan Plan { get; set; }
        public int PlanId { get; set; }
    }
}