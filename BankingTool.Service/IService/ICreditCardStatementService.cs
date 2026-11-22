using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    public interface ICreditCardStatementService
    {
        Task<ResponseDto<StatementDTO>> GetStatementWithDetails(int statementId);
        Task<ResponseDto<StatementDTO>> GetCurrentStatement(int creditCardId);
        Task<ResponseDto<List<StatementDTO>>> GetStatementsByCardId(int creditCardId);
    }
}
