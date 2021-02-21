using System.Collections.Generic;

namespace Restaurants.Models.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<OpenHoursDto> Schedule { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}