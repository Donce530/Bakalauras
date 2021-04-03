using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Reservations.Models.Data;

namespace Models.Restaurants.Models.Data
{
    [Table("PlanTables")]
    public class PlanTable : PlanItem
    {
        public int Seats { get; set; }
        public int Number { get; set; }

        public RestaurantPlan Plan { get; set; }
        public int PlanId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}