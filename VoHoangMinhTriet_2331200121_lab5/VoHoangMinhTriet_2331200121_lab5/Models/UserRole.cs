using System.ComponentModel.DataAnnotations.Schema;

namespace VoHoangMinhTriet_2331200121_lab5.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        public User user { get; set; }
        public Role role { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
