using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.CreditCard
{
    public class RecordCreditCardTransactionRequestObject
    {
        public RecordCreditCardTransactionRequest Request { get; set; }
    }
    public class RecordCreditCardTransactionRequest
    {
        public RecordCreditCardTransactionRequestCreditCard CreditCard { get; set; }
        public RecordCreditCardTransactionRequestTransaction Transaction { get; set; }
        public List<RecordCreditCardTransactionRequestBudgetAccount> BudgetAccounts { get; set; }
    }
    public class RecordCreditCardTransactionRequestCreditCard : RequestId
    {
    }
    public class RecordCreditCardTransactionRequestBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
    public class RecordCreditCardTransactionRequestTransaction
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
