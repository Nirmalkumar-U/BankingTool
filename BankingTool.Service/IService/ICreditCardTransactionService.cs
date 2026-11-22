using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardTransactionService
    {
        Task<ResponseDto<TransactionDTO>> RecordTransaction(int creditCardId, TransactionType transactionType,
            decimal amount, string description, string merchantName, List<BudgetAccountDto> budgetAccounts, DateTime transactionDate);
        Task<ResponseDto<TransactionDTO>> ProcessPayment(int creditCardId, decimal paymentAmount, string description, DateTime transactionDate, decimal fees);
        Task<ResponseDto<List<TransactionDTO>>> GetTransactionHistory(int creditCardId, int pageNumber = 1, int pageSize = 10);
        Task<ResponseDto<List<TransactionDTO>>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);
    }
}
