using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardStatementService
    {
        Task<ResponseDto<StatementResponseObject>> GetStatementWithDetails(int statementId);
        Task<ResponseDto<StatementResponseListObject>> GetStatementsByCardId(int creditCardId);
    }
}
