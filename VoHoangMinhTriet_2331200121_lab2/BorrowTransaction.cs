namespace VoHoangMinhTriet_2331200121_lab2
{
    class BorrowTransaction : Transaction
    {
        private Book _bookBorrowed;
        internal BorrowTransaction (string id, DateTime date, Member m, Book b) : base(id, date, m)
        {
            BookBorrowed = b;
        }
        public Book BookBorrowed
        {
            get { return _bookBorrowed; }
            set { _bookBorrowed = value ?? throw new Exception("Enter a valid Book"); }
        }
        public override void Execute()
        {
            if (BookBorrowed.CopiesAvailable <=0)
            {
                Console.WriteLine("out of books to borrow");
            }
            else Console.WriteLine("Successfully handeled book borrowing");
        }
    }
}
