using System.Collections.ObjectModel;

namespace VoHoangMinhTriet_2331200121_lab5.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // e.g., “Admin”, “User”, "Librarian" ,....
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
