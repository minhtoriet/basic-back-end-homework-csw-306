namespace lab4.Models.Request;

public class BookQueryParameters
{
    // Pagination defaults
    const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get;
        // Prevent clients from requesting 1,000,000 records at once
        set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }

    // Search and Filters
    public string? Title { get; set; }
    public int? CategoryId { get; set; }
    public int? AuthorId { get; set; }
}