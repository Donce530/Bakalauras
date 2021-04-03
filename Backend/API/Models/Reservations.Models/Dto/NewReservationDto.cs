using System;

namespace Models.Reservations.Models.Dto
{
    public class NewReservationDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}