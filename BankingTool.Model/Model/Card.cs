namespace BankingTool.Model
{
    public class Card : BaseSchema
    {
        public int CardId { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public int CardLimit { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CVV { get; set; }
    }
}
