namespace VoHoangMinhTriet_2331200121_lab3;
public class Book
{
    private string _isbn;
    private string _title;
    private string _author; 
    private int _year;
    private int _copiesAvailable;
    
    public string ISBN { get => _isbn; set => _isbn = value; }
    public string Title { get => _title; set => _title = value; }
    public string Author { get => _author; set => _author = value; }
    public Book(string i, string t, string a, int y, int c)
    {
        _isbn = i;
        _title = t;
        _author = a;
        Year = y;
        CopiesAvailable = c;
    }
    public int Year
    {
        get { return _year;}
        set
        {
            if (value < 0) throw new Exception("enter a valid year");
            _year = value;
        }
    }

    public int CopiesAvailable
    {
        get { return _copiesAvailable;}
        set
        {
            if (value < 0) throw new Exception("enter a valid copies available");
            _copiesAvailable = value;
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"ISBN: {_isbn}, title: {_title}, author: {_author}, year: {Year}, copies: {CopiesAvailable}"); 
    }
    
}

