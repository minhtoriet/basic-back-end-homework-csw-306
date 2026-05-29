namespace VoHoangMinhTriet_2331200121_lab5.Models.Request
{
    public class LoanQueryParameter
    {
        public int? UserId { get; set; } = null;
        public int? Status { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
