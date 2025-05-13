namespace BankingTool.Model.Model
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAbbrivation { get; set; }


        // Optional Navigation Property
        public virtual ICollection<Account> Account { get; set; }
    }
}
