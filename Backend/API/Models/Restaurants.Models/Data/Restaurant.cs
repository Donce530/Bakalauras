using System.Collections.Generic;
using Models.Reservations.Models.Data;
using Models.Users.Models.Dao;

namespace Models.Restaurants.Models.Data
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public IList<OpenHours> Schedule { get; set; }
        public UserDao User { get; set; }
        public int UserId { get; set; }
        public RestaurantPlan RestaurantPlan { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}