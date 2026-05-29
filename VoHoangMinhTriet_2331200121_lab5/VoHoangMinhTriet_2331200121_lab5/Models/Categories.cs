using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoHoangMinhTriet_2331200121_lab5.Models;

[Table("Categories")]
public class Categories
{

    public ICollection<Books> BookList { get; set; } = new List<Books>();
    [Key]
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

