namespace VoHoangMinhTriet_2331200121_lab5.Models.Request
{
    public class MostBorrowedBooksParameter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Top { get; set; }
    }
}
