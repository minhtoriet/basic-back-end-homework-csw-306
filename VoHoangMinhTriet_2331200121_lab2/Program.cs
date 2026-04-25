namespace VoHoangMinhTriet_2331200121_lab3;
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
        
    }
}