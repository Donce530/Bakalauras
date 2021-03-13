namespace Restaurants.Models.Dto
{
    public abstract class PlanItemDtoBase
    {
        public int Id { get; set; }
        public string Svg { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}