using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace lab4.Models;

public class Authors
{
    public ICollection<Books> BookList { get; set; }
    public int AuthorId { get; set; }
    [StringLength(100, ErrorMessage = "maximum 100 characters")]
    [Required]
    public string FirstName { get; set; } 
    [StringLength(100, ErrorMessage = "maximum 100 characters")]
    [Required]
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    [StringLength(100, ErrorMessage = "maximum 100 characters")]
    public string? Nationality { get; set; }
    [StringLength(100, ErrorMessage = "maximum 100 characters")]
    [EmailAddress]
    public string? Email { get; set; }
    [StringLength(100, ErrorMessage = "maximum 100 characters")]
    public string? Website { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public bool IsActive { get; set; } 
    public string? Avatar { get; set; }

}

