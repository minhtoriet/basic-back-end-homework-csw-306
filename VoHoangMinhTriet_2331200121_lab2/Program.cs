namespace VoHoangMinhTriet_2331200121_lab2;
static class Program
{
    static void Main()
    {
        Book book = new Book("120","120 days in hell","Trie",1999,3);
        book.DisplayInfo();
        Member m = new Member("123", "Trie", "adda@aa.com");
        PremiumMember pm = new PremiumMember("123", "Trie", "adda@aa.com",new DateTime(), 12);
        m.DisplayInfo();
        pm.DisplayInfo();
        List<Transaction> translist = new List<Transaction> ();
        translist.Add(new ReturnTransaction("1234", new DateTime(), m, book));
        translist.Add(new BorrowTransaction("1235", new DateTime(), m, book));
        foreach (Transaction t in translist)
        {
            t.Execute();
        }
        //exercise 6
        Library lb = new Library("not default name",new List<Book> {book}, new List<Member> {m,pm});
        lb.DisplayLibraryInfo();

        //Exercise 7
        NotificationService ns = new NotificationService();
        AdvancedNotificationService ans = new AdvancedNotificationService();
        ns.SendNotification("the message");
        ans.SendNotification("the message");

        //Exercise 9
        BookClass bc1 = new BookClass("13412313","title of a book","trie");
        BookClass bc2 = new BookClass("13412313", "title of a book", "trie");
        BookRecord br1 = new BookRecord("13412313", "title of a book", "trie");
        BookRecord br2 = new BookRecord("13412313", "title of a book", "trie");

        Console.WriteLine($"comparing two objects with == : { bc1 == bc2 } and two records with ==: {br1 == br2 }");
        BookRecord br3 = br2 with { Author = "trie1" };
        Console.WriteLine(br3.ToString());


        //Exercise 10
        lb.OnBookBorrowed += ns.NotifyBookBorrow;
        lb.BorrowBook(lb.Books[0], lb.Members[0]);

    }
}