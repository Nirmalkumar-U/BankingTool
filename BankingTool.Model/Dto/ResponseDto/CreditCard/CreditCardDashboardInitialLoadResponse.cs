namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardDashboardInitialLoadResponse
    {
        public List<CreditCardDTO> CreditCards { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
    }
}
