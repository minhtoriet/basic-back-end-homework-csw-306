using Microsoft.EntityFrameworkCore;

namespace VoHoangMinhTriet_2331200121_lab5.Models.Context
{

    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Books> Book { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Authors> Author { get; set; }
        public DbSet<Loans> Loan { get; set; }
        public DbSet<Carousels> Carousel { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });


        }
    }
}
