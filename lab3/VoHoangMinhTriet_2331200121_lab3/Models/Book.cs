namespace VoHoangMinhTriet_2331200121_lab3.Model;

public class Book
{
    private int _id;
    private string _title;
    private string _author;
    private int _year;
    private string _genre;

    public Book(int id, string title, string author, int year, string genre)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        Genre = genre;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
}