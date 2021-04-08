using System.Collections.Generic;

namespace Models.Restaurants.Models.Dto
{
    public class TableDto : PlanItemDtoBase
    {
        public int Seats { get; set; }
        public int Number { get; set; }
        public ICollection<int> LinkedTableNumbers { get; set; }
    }
}