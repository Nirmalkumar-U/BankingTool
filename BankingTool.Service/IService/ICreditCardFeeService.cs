using BankingTool.Model;
using BankingTool.Model.Dto.Response;

namespace BankingTool.Service.IService
{
    /// <summary>
    /// Service interface for credit card fee operations
    /// </summary>
    public interface ICreditCardFeeService
    {
        /// <summary>
        /// Applies an annual fee to a credit card
        /// </summary>
        Task<ResponseDto<bool>> ApplyAnnualFee(int creditCardId, decimal feeAmount);

        /// <summary>
        /// Applies a late payment fee
        /// </summary>
        Task<ResponseDto<bool>> ApplyLatePaymentFee(int creditCardId, decimal feeAmount, int daysLate);

        /// <summary>
        /// Applies an over-limit fee
        /// </summary>
        Task<ResponseDto<bool>> ApplyOverLimitFee(int creditCardId, decimal feeAmount);

        /// <summary>
        /// Applies a transaction fee
        /// </summary>
        Task<ResponseDto<bool>> ApplyTransactionFee(int creditCardId, decimal feeAmount, string reason);

        /// <summary>
        /// Applies a cash advance fee
        /// </summary>
        Task<ResponseDto<bool>> ApplyCashAdvanceFee(int creditCardId, decimal feeAmount, decimal cashAdvanceAmount);

        /// <summary>
        /// Gets all fees for a card
        /// </summary>
        Task<ResponseDto<List<CreditCardFee>>> GetCardFees(int creditCardId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Gets fees by type
        /// </summary>
        Task<ResponseDto<List<CreditCardFee>>> GetFeesByType(int creditCardId, string feeType);

        /// <summary>
        /// Calculates total fees for a card in a date range
        /// </summary>
        Task<ResponseDto<decimal>> CalculateTotalFees(int creditCardId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets fees for current billing cycle
        /// </summary>
        Task<ResponseDto<List<CreditCardFee>>> GetBillingCycleFees(int creditCardId, DateTime cycleStartDate, DateTime cycleEndDate);

        /// <summary>
        /// Removes a fee (reverses a fee charge)
        /// </summary>
        Task<ResponseDto<bool>> ReverseFee(int feeId, string reason);

        /// <summary>
        /// Waives a fee (admin operation)
        /// </summary>
        Task<ResponseDto<bool>> WaiveFee(int feeId, string reason);
    }
}
