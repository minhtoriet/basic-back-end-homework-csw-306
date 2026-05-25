namespace lab4.Models.Request
{
    public class MostBorrowedBooksDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int BorrowCount { get; set; }
    }
}
