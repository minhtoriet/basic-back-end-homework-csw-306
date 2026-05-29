namespace VoHoangMinhTriet_2331200121_lab5.Models.Request
{
    public class AuthorCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
