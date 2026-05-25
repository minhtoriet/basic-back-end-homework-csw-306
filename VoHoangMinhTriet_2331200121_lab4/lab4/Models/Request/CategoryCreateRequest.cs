namespace lab4.Models.Request;

public class CategoryCreateRequest 
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? Avatar { get; set; }
}