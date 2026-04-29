namespace VoHoangMinhTriet_2331200121_lab2;

internal class ReturnTransaction : Transaction
{
    private Book _bookReturned;
    internal ReturnTransaction (string id, DateTime date, Member m, Book b) : base(id, date, m)
    {
        BookReturned = b;
    }
    public Book BookReturned 
    { 
        get { return _bookReturned; }  
        set { _bookReturned = value ?? throw new Exception("input a valid book"); } 
    }
    public override void Execute()
    {
        BookReturned.CopiesAvailable += 1;
        Console.WriteLine("Successfully handled book returning");
    }
}

