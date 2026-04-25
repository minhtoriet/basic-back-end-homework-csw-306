namespace VoHoangMinhTriet_2331200121_lab2;

public abstract class Transaction
{
    private string _transactionid;
    private DateTime _transactionDate;
    private Member _member;

    public Transaction(string id, DateTime date, Member m)
    {
        _transactionid = id;
        _transactionDate = date;
        _member = m;
    }

    public abstract void Execute();
}