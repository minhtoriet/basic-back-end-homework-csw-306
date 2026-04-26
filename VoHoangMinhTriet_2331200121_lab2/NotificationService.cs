namespace VoHoangMinhTriet_2331200121_lab2
{
    internal class NotificationService
    {
        public virtual void SendNotification(string message)
        {
            Console.WriteLine(message);
        }
        public void SendNotification(string message, string recipient)
        {
            Console.WriteLine($"{recipient}:{message}");
        }
        public void SendNotification(string message, List<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                Console.WriteLine($"{recipient} {message}");
            }
        }
        public void NotifyBookBorrow(Book b, Member m)
        {
            Console.WriteLine($"notification: {m.Name} borrowed {b.Title}");
        }

    }
}
