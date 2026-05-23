namespace lab4.Models.Request;

public class BookCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Publisher { get; set; }
    public DateTime PublishedYear { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    
    public IFormFile? CoverImage { get; set; }
    public IFormFile? PdfFile { get; set; }
}