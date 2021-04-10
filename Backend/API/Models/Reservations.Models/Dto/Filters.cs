using System;

namespace Models.Reservations.Models.Dto
{
    public class Filters
    {
        public string Name { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? StartAfter { get; set; }
        public DateTime? StartUntil { get; set; }
        public DateTime? EndAfter { get; set; }
        public DateTime? EndUntil { get; set; }
        public int? TableNumber { get; set; }
    }
}