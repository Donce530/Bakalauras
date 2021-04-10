namespace Models.Reservations.Models.Dto
{
    public class PagedFilteredParams
    {
        public Paginator Paginator { get; set; }
        public Filters Filters { get; set; }
    }
}