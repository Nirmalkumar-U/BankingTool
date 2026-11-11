using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    /// <summary>
    /// Service interface for credit card transaction operations
    /// </summary>
    public interface ICreditCardTransactionService
    {
        /// <summary>
        /// Records a new transaction (purchase or fee)
        /// </summary>
        Task<ResponseDto<TransactionDTO>> RecordTransaction(int creditCardId, TransactionType transactionType, decimal amount, 
            string description, string merchantName, string referenceNumber);

        /// <summary>
        /// Processes a payment on the credit card
        /// </summary>
        Task<ResponseDto<TransactionDTO>> ProcessPayment(int creditCardId, decimal paymentAmount, string description);

        /// <summary>
        /// Reverses a transaction
        /// </summary>
        Task<ResponseDto<bool>> ReverseTransaction(int transactionId, string reason);

        /// <summary>
        /// Gets transaction history for a card
        /// </summary>
        Task<ResponseDto<List<TransactionDTO>>> GetTransactionHistory(int creditCardId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Gets transactions within a date range
        /// </summary>
        Task<ResponseDto<List<TransactionDTO>>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets transactions by type
        /// </summary>
        Task<ResponseDto<List<TransactionDTO>>> GetTransactionsByType(int creditCardId, TransactionType transactionType);

        /// <summary>
        /// Calculates available credit for a card
        /// </summary>
        Task<ResponseDto<decimal>> CalculateAvailableCredit(int creditCardId);

        /// <summary>
        /// Gets recent transactions (last N transactions)
        /// </summary>
        Task<ResponseDto<List<TransactionDTO>>> GetRecentTransactions(int creditCardId, int count = 10);

        /// <summary>
        /// Gets transaction details by ID
        /// </summary>
        Task<ResponseDto<TransactionDTO>> GetTransactionDetails(int transactionId);
    }
}
