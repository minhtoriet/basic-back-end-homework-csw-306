using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoHoangMinhTriet_2331200121_lab5.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "role name of 50 characters")]
        public string RoleName { get; set; } // e.g., “Admin”, “User”, "Librarian" ,....
        [StringLength(200, ErrorMessage = "role name of 200 characters")]
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
