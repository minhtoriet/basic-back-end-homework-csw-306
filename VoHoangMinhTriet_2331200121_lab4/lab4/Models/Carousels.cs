using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab4.Models
{
    [Table("Carousels")]
    public class Carousels
    {
        [Key]
        public int CarouselId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "maximum 200 characters")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? LinkUrl { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
