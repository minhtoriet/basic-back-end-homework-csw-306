namespace VoHoangMinhTriet_2331200121_lab2
{
    internal class AdvancedNotificationService : NotificationService
    {
        public override void SendNotification(string message)
        {
            Console.WriteLine($"{message}, sent on {DateTime.Now.ToString()}");
        }
    }
}
