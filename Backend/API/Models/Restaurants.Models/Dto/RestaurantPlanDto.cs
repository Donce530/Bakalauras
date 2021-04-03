namespace Models.Restaurants.Models.Dto
{
    public class RestaurantPlanDto
    {
        public int Id { get; set; }
        public string WebSvg { get; set; }
        public WallDto[] Walls { get; set; }
        public TableDto[] Tables { get; set; }
    }
}