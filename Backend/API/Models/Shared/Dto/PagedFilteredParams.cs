namespace Models.Reservations.Models.Dto
{
    public class PagedFilteredParams<TFilter>
    {
        public Paginator Paginator { get; set; }
        public TFilter Filters { get; set; }
    }
}