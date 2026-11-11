using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardTransactionRepository
    {
        /// <summary>
        /// Gets a transaction by ID
        /// </summary>
        Task<CreditCardTransaction> GetTransactionById(int transactionId);

        /// <summary>
        /// Gets all transactions for a credit card
        /// </summary>
        Task<List<CreditCardTransaction>> GetTransactionsByCardId(int creditCardId);

        /// <summary>
        /// Gets transactions for a card with pagination
        /// </summary>
        Task<List<CreditCardTransaction>> GetTransactionsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize);

        /// <summary>
        /// Gets transactions within a date range
        /// </summary>
        Task<List<CreditCardTransaction>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets transactions by type (Purchase, Payment, Fee, Interest, etc.)
        /// </summary>
        Task<List<CreditCardTransaction>> GetTransactionsByType(int creditCardId, TransactionType transactionType);

        /// <summary>
        /// Gets transactions by status (Completed, Pending, Reversed, Failed)
        /// </summary>
        Task<List<CreditCardTransaction>> GetTransactionsByStatus(int creditCardId, TransactionStatus status);

        /// <summary>
        /// Creates a new transaction
        /// </summary>
        Task<int> InsertTransaction(CreditCardTransaction transaction);

        /// <summary>
        /// Updates an existing transaction
        /// </summary>
        Task<bool> UpdateTransaction(CreditCardTransaction transaction);

        /// <summary>
        /// Reverses a transaction
        /// </summary>
        Task<bool> ReverseTransaction(int transactionId, string reason);

        /// <summary>
        /// Calculates total charges for a card
        /// </summary>
        Task<decimal> CalculateTotalCharges(int creditCardId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Calculates total payments for a card
        /// </summary>
        Task<decimal> CalculateTotalPayments(int creditCardId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all transactions (admin only)
        /// </summary>
        Task<List<CreditCardTransaction>> GetAllTransactions();

        /// <summary>
        /// Gets recent transactions for a card
        /// </summary>
        Task<List<CreditCardTransaction>> GetRecentTransactions(int creditCardId, int count);

        /// <summary>
        /// Deletes a transaction
        /// </summary>
        Task<bool> DeleteTransaction(int transactionId);
    }
}
