using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoHoangMinhTriet_2331200121_lab5.Models;

[Table("Loans")]
public class Loans
{

    public Books Book { get; set; }
    public User User { get; set; }
    [Key]
    public int LoanId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    [Required]
    public int Status { get; set; }
}

