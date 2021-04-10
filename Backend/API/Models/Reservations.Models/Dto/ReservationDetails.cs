using System;
using System.Collections.Generic;

namespace Models.Reservations.Models.Dto
{
    public class ReservationDetails
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public int TableId { get; set; }
        public int TableSeats { get; set; }
        public int TableNumber { get; set; }
        public ICollection<ReservationDetails> LinkedTableDetails { get; set; }

        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}