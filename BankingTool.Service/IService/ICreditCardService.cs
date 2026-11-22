using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardService
    {
        Task<ResponseDto<CreditCardApplicationResponseDTO>> ApplyCreditCard(CreditCardApplicationRequestDTO request);

        Task<ResponseDto<bool>> UpdateCardLimit(int creditCardId, decimal newLimit);

        Task<ResponseDto<bool>> CloseCreditCard(int creditCardId, string closureReason);

        Task<ResponseDto<bool>> BlockCreditCard(int creditCardId, string blockReason);

        Task<ResponseDto<bool>> UnblockCreditCard(int creditCardId);

        Task<ResponseDto<CreditCardDTO>> GetCardDetails(int creditCardId);

        Task<ResponseDto<CreditCardDashboardInitialLoadResponse>> GetCreditCardDashboardInitialLoad();
        Task<ResponseDto<List<CreditCardDTO>>> GetCustomerCreditCards();
    }
}
