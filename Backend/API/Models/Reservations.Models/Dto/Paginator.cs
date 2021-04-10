namespace Models.Reservations.Models.Dto
{
    public class Paginator
    {
        public int Rows { get; set; }
        public int Offset { get; set; }
        public string SortBy { get; set; }
        public int SortOrder { get; set; }
        public int TotalRows { get; set; }
    }
}