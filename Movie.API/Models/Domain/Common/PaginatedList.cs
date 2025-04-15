namespace Movie.API.Models.Domain.Common
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; set; } = default!;
        public int TotalPages { get; set; } = default!;
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public List<T> Items { get; set; } = default!;

        public PaginatedList(List<T> items, int pageNumber, int totalPages)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }
        public PaginatedList()
        {
            Items = new List<T>();
        }
    }
}
