using System;

namespace Models.Reservations.Models.Dto
{
    public class ReservationFilters
    {
        public string Name { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? StartAfter { get; set; }
        public DateTime? StartUntil { get; set; }
        public DateTime? EndAfter { get; set; }
        public DateTime? EndUntil { get; set; }
        
        public DateTime? RealStartAfter { get; set; }
        public DateTime? RealStartUntil { get; set; }
        public DateTime? RealEndAfter { get; set; }
        public DateTime? RealEndUntil { get; set; }
        public int? TableNumber { get; set; }
    }
}