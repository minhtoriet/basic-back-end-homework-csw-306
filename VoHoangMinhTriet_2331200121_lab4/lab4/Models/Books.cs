using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab4.Models;

[Table("Books")]
public class Books
{
    public ICollection<Loans> LoanList { get; set; } = new List<Loans>();
    public Authors Author { get; set; }
    public Categories Category { get; set; }
    [Key]
    
    public int BookId { get; set; }
    [Required]
    public string Title { get; set; }
    [StringLength(200, ErrorMessage = "maximum 200 characters")]
    [Required]
    public string Description { get; set; }
    [Required]
    public string Publisher { get; set; }
    [Required]
    public DateTime PublishedYear { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public int TotalCopies { get; set; }
    [Required]
    public int AvailableCopies { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public string? Avatar { get; set; }
    public string? Pdf { get; set; }
}

