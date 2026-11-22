using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardTransactionRepository
    {
        Task<CreditCardTransaction> GetTransactionById(int transactionId);

        Task<List<CreditCardTransaction>> GetTransactionsByCardId(int creditCardId);

        Task<List<CreditCardTransaction>> GetTransactionsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize);

        Task<List<CreditCardTransaction>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);

        Task<List<CreditCardTransaction>> GetTransactionsByType(int creditCardId, TransactionType transactionType);

        Task<List<CreditCardTransaction>> GetTransactionsByStatus(int creditCardId, TransactionStatus status);

        Task<int> InsertTransaction(CreditCardTransaction transaction);

        Task<(bool Success, int TransactionId)> SaveCreditCardTransaction(CreditCardTransaction transaction, CreditCard creditCard, List<BudgetTransactionDto> budgetTransactions);

        Task<bool> ProcessPaymentWithStatement(CreditCardTransaction transaction, CreditCard creditCard, CreditCardStatement statement, List<CreditCardTransaction> transactions);

        Task<bool> UpdateTransaction(CreditCardTransaction transaction);

        Task<decimal> CalculateTotalCharges(int creditCardId, DateTime startDate, DateTime endDate);

        Task<decimal> CalculateTotalPayments(int creditCardId, DateTime startDate, DateTime endDate);

        Task<List<TransactionDTO>> GetAllTransactionsOfCustomer();

        Task<bool> DeleteTransaction(int transactionId);
    }
}
