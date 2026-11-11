namespace BankingTool.Model.Dto.RequestDto.CreditCard
{
    public class CreditCardApplicationRequestDTO
    {
        public int AccountId { get; set; }
        public string CardType { get; set; }
        public decimal DesiredLimit { get; set; }
        public string IncomeDocuments { get; set; }
    }
}
