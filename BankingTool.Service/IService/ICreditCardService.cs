using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.CreditCard;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardService
    {
        Task<ResponseDto<bool>> ApplyCreditCard(CreditCardApplicationRequest request);
        Task<ResponseDto<bool>> UpdateCardLimit(int creditCardId, decimal newLimit);
        Task<ResponseDto<bool>> CloseCreditCard(int creditCardId, string closureReason);
        Task<ResponseDto<bool>> BlockCreditCard(int creditCardId, string blockReason);
        Task<ResponseDto<bool>> UnblockCreditCard(int creditCardId);
        Task<ResponseDto<CreditCardResponseObject>> GetCardDetails(int creditCardId);
        Task<ResponseDto<CreditCardDashboardInitialLoadResponse>> GetCreditCardDashboardInitialLoad();
        Task<ResponseDto<CreditCardResponseListObject>> GetCustomerCreditCards();
    }
}
