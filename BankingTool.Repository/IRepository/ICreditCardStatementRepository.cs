using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardStatementRepository
    {
        Task<CreditCardStatement> GetStatementById(int statementId);
        Task<List<CreditCardStatement>> GetStatementsByCardId(int creditCardId);
        Task<List<CreditCardStatement>> GetStatementsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize);
        Task<CreditCardStatement> GetCurrentStatement(int creditCardId);
        Task<List<CreditCardStatement>> GetStatementsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);
        Task<List<CreditCardStatement>> GetStatementsByStatus(int creditCardId, StatementStatus status);
        Task<int> InsertStatement(CreditCardStatement statement);
        Task<bool> UpdateStatement(CreditCardStatement statement);
        Task<int> GenerateStatement(int creditCardId, DateTime billingCycleEndDate);
        Task<List<CreditCardStatement>> GetAllStatements();
        Task<bool> DeleteStatement(int statementId);
        Task<List<CreditCardStatementDetail>> GetStatementDetails(int statementId);
        Task<int> InsertStatementDetail(CreditCardStatementDetail statementDetail);
    }
}
