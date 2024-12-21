namespace Core.DTOs
{
    public class PaginationDto<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
    }
}
