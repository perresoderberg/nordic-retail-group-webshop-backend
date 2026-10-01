namespace nordicretailgroup.webshop.Application.DTOs
{
    public sealed class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = [];

        public int TotalCount { get; init; }
    }
}