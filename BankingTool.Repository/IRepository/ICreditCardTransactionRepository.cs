using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardTransactionRepository
    {
        Task<List<CreditCardTransaction>> GetTransactionsByCardId(int creditCardId);
        Task<List<CreditCardTransaction>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);
        Task<(bool Success, int TransactionId)> SaveCreditCardTransaction(CreditCardTransaction transaction, CreditCard creditCard, List<BudgetTransactionDto> budgetTransactions);
        Task<bool> ProcessPaymentWithStatement(CreditCardTransaction transaction, CreditCard creditCard, CreditCardStatement statement, List<CreditCardTransaction> transactions);
        Task<List<TransactionResponse>> GetAllTransactionsOfCustomer();
    }
}
