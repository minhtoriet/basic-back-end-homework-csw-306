namespace VoHoangMinhTriet_2331200121_lab2
{
    internal class LibraryCard
    {
        private string _cardNumber;
        private Member _owner;
        private DateTime _issueDate;

        internal LibraryCard(string cardnum, Member owner) 
        {
            _cardNumber = cardnum;
            Owner = owner; 
            IssueDate = DateTime.Now;
        }
        public string CardNumber { get { return _cardNumber; } }
        public Member Owner { get { return _owner; } set { _owner = value; } }
        public DateTime IssueDate { get { return _issueDate; } private set { _issueDate = value; } }

        public void RenewCard()
        {
            IssueDate = DateTime.Now;
        }


    }
}
