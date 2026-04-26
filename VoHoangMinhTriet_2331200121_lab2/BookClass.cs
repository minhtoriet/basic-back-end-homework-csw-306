namespace VoHoangMinhTriet_2331200121_lab2
{
    internal class BookClass
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookClass()
        {

        }
        public BookClass(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }
    }
}
