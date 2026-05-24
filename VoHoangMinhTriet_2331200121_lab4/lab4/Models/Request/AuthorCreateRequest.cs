namespace lab4.Models.Request
{
    public class AuthorCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
