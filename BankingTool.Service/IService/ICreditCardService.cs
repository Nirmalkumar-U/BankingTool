using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    /// <summary>
    /// Service interface for credit card management operations
    /// </summary>
    public interface ICreditCardService
    {
        /// <summary>
        /// Applies for a new credit card
        /// </summary>
        Task<ResponseDto<CreditCardApplicationResponseDTO>> ApplyCreditCard(CreditCardApplicationRequestDTO request);

        /// <summary>
        /// Approves a credit card application
        /// </summary>
        Task<ResponseDto<bool>> ApproveCreditCard(int applicationId, decimal approvedLimit, string approvalRemarks);

        /// <summary>
        /// Rejects a credit card application
        /// </summary>
        Task<ResponseDto<bool>> RejectCreditCard(int applicationId, string rejectionRemarks);

        /// <summary>
        /// Updates the credit limit for a card
        /// </summary>
        Task<ResponseDto<bool>> UpdateCardLimit(int creditCardId, decimal newLimit);

        /// <summary>
        /// Closes an active credit card
        /// </summary>
        Task<ResponseDto<bool>> CloseCreditCard(int creditCardId, string closureReason);

        /// <summary>
        /// Blocks a credit card (temporary suspension)
        /// </summary>
        Task<ResponseDto<bool>> BlockCreditCard(int creditCardId, string blockReason);

        /// <summary>
        /// Unblocks a credit card
        /// </summary>
        Task<ResponseDto<bool>> UnblockCreditCard(int creditCardId);

        /// <summary>
        /// Gets credit card details with masked sensitive data
        /// </summary>
        Task<ResponseDto<CreditCardDTO>> GetCardDetails(int creditCardId);

        /// <summary>
        /// Gets all active credit cards for a customer
        /// </summary>
        Task<ResponseDto<List<CreditCardDTO>>> GetCustomerCreditCards();

        /// <summary>
        /// Gets application status by application ID
        /// </summary>
        Task<ResponseDto<CreditCardApplicationResponseDTO>> GetApplicationStatus(int applicationId);

        /// <summary>
        /// Gets all applications for a customer
        /// </summary>
        Task<ResponseDto<List<CreditCardApplicationResponseDTO>>> GetCustomerApplications(int customerId);

        /// <summary>
        /// Gets all pending applications (for admin/officer)
        /// </summary>
        Task<ResponseDto<List<CreditCardApplicationResponseDTO>>> GetPendingApplications();
    }
}
