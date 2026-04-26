namespace VoHoangMinhTriet_2331200121_lab2
{
    internal class Library
    {
        private string _libraryName;
        private List<Book> _books;
        private List<Member> _members;
        public event Action<Book, Member>? OnBookBorrowed;

        internal Library() 
        {
            LibraryName = "Default Lib name";
            Books = new List<Book>();
            Members = new List<Member>();
        }
        internal Library(string name, List<Book> books)
        {
            LibraryName = name;
            Books = books;
            Members = new List<Member>();
        }
        internal Library (string name, List<Book> books, List<Member> members)
        {
            LibraryName = name;
            Books = books;
            Members = members;
        }
        public string LibraryName { get { return _libraryName; } set { _libraryName = value; } }
        public List<Book> Books { get { return _books; } set {_books = value; } }
        public List<Member> Members { get { return _members; } set {_members = value; } }
        
        public void BorrowBook(Book b, Member m)
        {
            Console.WriteLine($"{m.Name} borrowed {b.Title}");
            OnBookBorrowed?.Invoke(b, m);
        }
        public Library CopyLibrary() 
        {
            return new Library(LibraryName, Books, Members);
        }
        public void DisplayLibraryInfo()
        {
            Console.WriteLine($"lib name: {LibraryName}, num of books: {Books.Count}, num of members: {Members.Count}");
        }
    }

}
