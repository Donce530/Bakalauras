using System.Collections.Generic;
using Users.Models.Dao;

namespace Restaurants.Models.Data
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
    }
}