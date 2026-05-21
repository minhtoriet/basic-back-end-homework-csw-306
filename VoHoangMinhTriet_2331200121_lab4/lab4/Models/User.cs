using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace lab4.Models;

public class User
{
    
    public ICollection<Loans> LoanList { get; set; }
    public int UserId { get; set; }
    [StringLength(200, ErrorMessage = "maximun 200 characters")]
    [Required]
    public string FullName { get; set; }
    public string Description { get; set; }
    [Required]
    public string Password { get; set; }
    [StringLength(100,ErrorMessage = "maximum 100 characters")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(20, ErrorMessage ="maximum 20 characters")]
    public string Phone { get; set; }
    public string Address { get; set; }
    [Required]
    public int Status { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public string UserCode { get; set; }
    [Required]
    public bool IsLocked { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
    [Required]
    public bool IsActive { get; set; } 
    [Required]
    public string ActiveCode { get; set; }
    public string Avatar { get; set; }
}

