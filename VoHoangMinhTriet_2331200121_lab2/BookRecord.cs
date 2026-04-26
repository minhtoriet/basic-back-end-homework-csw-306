namespace VoHoangMinhTriet_2331200121_lab2
{
    internal record BookRecord
    {
        public string ISBN;
        public string Title;
        public string Author;
        public BookRecord (string ISBN, string Title, string Author)
        {
            this.ISBN = ISBN;
            this.Title = Title;
            this.Author = Author;

        }
    }
}
