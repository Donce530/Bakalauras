using System;

namespace Models.Reservations.Models.Dto
{
    public class ReservationDataRow
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TableNumber { get; set; }
    }
}