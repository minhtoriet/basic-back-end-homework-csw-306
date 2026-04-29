namespace VoHoangMinhTriet_2331200121_lab2;

public class Member : IPrintable, IMemberActions
{
    private string _memberid;
    private string _name;
    private string _email;
    public event Action<Book, Member>? OnBookBorrowed;

    public Member() {}

    public Member(string m, string n, string e)
    {
        Memberid = m;
        Name = n;
        Email = e;
    }
    public string Memberid
    {
        get => _memberid;
        set => _memberid = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"member id: {Memberid}, name: {Name}, email: {Email}");
    }

    public void PrintDetails()
    {
        Console.WriteLine($"member id: {Memberid}, name: {Name}, email: {Email}");
    }
    public void BorrowBook(Book book)
    {

        Console.WriteLine($"{this.Name} borrowed {book.Title}");

        BorrowTransaction bt = new BorrowTransaction("DefaultID", DateTime.Now, this, book);
        bt.Execute();
        Console.WriteLine($"{book.DisplayInfo} successfully borrowed");
        OnBookBorrowed?.Invoke(book, this);
    }
    public void ReturnBook(Book book)
    {
        Console.WriteLine($"{book.DisplayInfo} successfully returned");
    }
}