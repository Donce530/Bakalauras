using System;

namespace Models.Reservations.Models.Dto
{
    public class ReservationListItemDto
    {
        public int Id { get; set; }
        public string RestaurantTitle { get; set; }
        public string RestaurantAddress { get; set; }
        public int TableNumber { get; set; }
        public int TableSeats { get; set; }
        public DateTime Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}