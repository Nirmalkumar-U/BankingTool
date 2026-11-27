using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardTransactionService
    {
        Task<ResponseDto<bool>> RecordTransaction(int creditCardId, TransactionType transactionType,
            decimal amount, string description, string merchantName, List<RecordCreditCardTransactionRequestBudgetAccount> budgetAccounts, DateTime transactionDate);
        Task<ResponseDto<bool>> ProcessPayment(int creditCardId, decimal paymentAmount, string description, DateTime transactionDate, decimal fees);
        Task<ResponseDto<TransactionResponseListObject>> GetTransactionHistory(int creditCardId);
    }
}
