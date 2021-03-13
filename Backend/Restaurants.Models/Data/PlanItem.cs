using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Models.Data
{
    [Table("PlanItems")]
    public class PlanItem
    {
        public int Id { get; set; }
        public string Svg { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
