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
    }
}
