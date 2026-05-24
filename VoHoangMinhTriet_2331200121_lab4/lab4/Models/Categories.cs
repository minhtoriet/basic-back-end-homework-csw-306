using System.ComponentModel.DataAnnotations;

namespace lab4.Models;

public class Categories
{
 
    public ICollection<Books> BookList { get; set; }
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public string? Avatar { get; set; }
}

