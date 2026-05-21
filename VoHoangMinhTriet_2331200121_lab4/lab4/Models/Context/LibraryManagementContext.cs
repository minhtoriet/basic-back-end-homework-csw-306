using Microsoft.EntityFrameworkCore;

namespace lab4.Models.Context
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
    }
}
