using System.Collections.Generic;

namespace Models.Reservations.Models.Dto
{
    public class PagedResponse<T>
    {
        public Paginator Paginator { get; set; }
        public IList<T> Results { get; set; }
    }
}