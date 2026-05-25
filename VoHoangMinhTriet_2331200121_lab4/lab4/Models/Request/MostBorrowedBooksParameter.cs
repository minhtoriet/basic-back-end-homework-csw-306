namespace lab4.Models.Request
{
    public class MostBorrowedBooksParameter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Top { get; set; }
    }
}
