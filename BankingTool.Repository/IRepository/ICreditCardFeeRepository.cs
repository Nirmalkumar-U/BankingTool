using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardFeeRepository
    {
        /// <summary>
        /// Gets a fee by ID
        /// </summary>
        Task<CreditCardFee> GetFeeById(int feeId);

        /// <summary>
        /// Gets all fees for a credit card
        /// </summary>
        Task<List<CreditCardFee>> GetFeesByCardId(int creditCardId);

        /// <summary>
        /// Gets fees for a card with pagination
        /// </summary>
        Task<List<CreditCardFee>> GetFeesByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize);

        /// <summary>
        /// Gets fees by type (Annual, LatePay, OverLimit, TransactionFee, InterestCharge, etc.)
        /// </summary>
        Task<List<CreditCardFee>> GetFeesByType(int creditCardId, string feeType);

        /// <summary>
        /// Gets fees within a date range
        /// </summary>
        Task<List<CreditCardFee>> GetFeesByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets fees for current billing cycle
        /// </summary>
        Task<List<CreditCardFee>> GetFeesByBillingCycle(int creditCardId, DateTime cycleStartDate, DateTime cycleEndDate);

        /// <summary>
        /// Gets recent fees for a card
        /// </summary>
        Task<List<CreditCardFee>> GetRecentFees(int creditCardId, int count);

        /// <summary>
        /// Creates a new fee
        /// </summary>
        Task<int> InsertFee(CreditCardFee fee);

        /// <summary>
        /// Updates an existing fee
        /// </summary>
        Task<bool> UpdateFee(CreditCardFee fee);

        /// <summary>
        /// Calculates total fees for a card
        /// </summary>
        Task<decimal> CalculateTotalFees(int creditCardId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all fees (admin only)
        /// </summary>
        Task<List<CreditCardFee>> GetAllFees();

        /// <summary>
        /// Deletes a fee
        /// </summary>
        Task<bool> DeleteFee(int feeId);

        /// <summary>
        /// Gets predicted/projected fees for next cycle
        /// </summary>
        Task<List<CreditCardFee>> GetProjectedFees(int creditCardId);
    }
}
