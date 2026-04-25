namespace VoHoangMinhTriet_2331200121_lab3;

public class Member
{
    private string _memberid;
    private string _name;
    private string _email;

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
}