using BankingTool.Model.Dto.InternalDto;

namespace BankingTool.Model.Dto.RequestDto.CreditCard
{
    public class RecordCreditCardTransactionRequestDTO
    {
        public int CreditCardId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }
        public List<BudgetAccountDto> BudgetAccounts { get; set; } = new List<BudgetAccountDto>();
        public DateTime TransactionDate { get; set; }
    }
}
