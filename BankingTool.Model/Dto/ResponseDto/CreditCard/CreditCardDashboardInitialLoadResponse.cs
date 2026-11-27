namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardDashboardInitialLoadResponse
    {
        public List<CreditCardResponse> CreditCards { get; set; }
        public List<TransactionResponse> Transactions { get; set; }
    }
}
