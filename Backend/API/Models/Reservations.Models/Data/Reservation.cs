using System;
using Models.Restaurants.Models.Data;
using Models.Users.Models.Dao;

namespace Models.Reservations.Models.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int TableId { get; set; }
        public PlanTable Table { get; set; }
        public int UserId { get; set; }
        public UserDao User { get; set; }
    }
}