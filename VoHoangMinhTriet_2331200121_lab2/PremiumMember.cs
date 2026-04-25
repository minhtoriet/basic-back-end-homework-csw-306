using System.Transactions;

namespace VoHoangMinhTriet_2331200121_lab2;

public class PremiumMember : Member
{
    private DateTime _membershipExpiry;
    private int _maxBooksAllowed;

    public PremiumMember(string m, string n, string e, DateTime d, int max) : base( m, n, e)
    {
        MembershipExpiry = d;
        MaxBooksAllowed = max;
    }
    public DateTime MembershipExpiry
    {
        get => _membershipExpiry;
        set => _membershipExpiry = value;
    }

    public int MaxBooksAllowed
    {
        get => _maxBooksAllowed;
        set => _maxBooksAllowed = value;
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"member id: {Memberid}, name: {Name}, email: {Email},\n " +
                          $"membership expiry: {MembershipExpiry}, max books allowed: {MaxBooksAllowed}");
    }
}