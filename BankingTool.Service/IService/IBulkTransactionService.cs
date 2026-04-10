using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BulkTransaction;

namespace BankingTool.Service.IService
{
    public interface IBulkTransactionService
    {
        Task<ResponseDto<bool>> BulkTransaction(BulkTransactionRequestObject model);
        Task<ResponseDto<bool>> GetBulkTransactionInitialLoad();
    }
}
