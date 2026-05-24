namespace lab4.Models.Request
{
    public class UserCreateRequest
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { set; get; }
    }
}
