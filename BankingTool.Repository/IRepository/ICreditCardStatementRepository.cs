using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardStatementRepository
    {
        Task<CreditCardStatement> GetStatementById(int statementId);
        Task<List<CreditCardStatement>> GetStatementsByCardId(int creditCardId);
        Task<CreditCardStatement> GetCurrentStatement(int creditCardId);
        Task<List<CreditCardStatementDetail>> GetStatementDetails(int statementId);
    }
}
