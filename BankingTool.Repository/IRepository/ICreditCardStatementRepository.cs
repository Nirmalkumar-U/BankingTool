using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardStatementRepository
    {
        /// <summary>
        /// Gets a statement by ID
        /// </summary>
        Task<CreditCardStatement> GetStatementById(int statementId);

        /// <summary>
        /// Gets all statements for a credit card
        /// </summary>
        Task<List<CreditCardStatement>> GetStatementsByCardId(int creditCardId);

        /// <summary>
        /// Gets statements with pagination
        /// </summary>
        Task<List<CreditCardStatement>> GetStatementsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the current/latest statement for a card
        /// </summary>
        Task<CreditCardStatement> GetCurrentStatement(int creditCardId);

        /// <summary>
        /// Gets statements within a date range
        /// </summary>
        Task<List<CreditCardStatement>> GetStatementsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets statements by status
        /// </summary>
        Task<List<CreditCardStatement>> GetStatementsByStatus(int creditCardId, StatementStatus status);

        /// <summary>
        /// Gets overdue statements
        /// </summary>
        Task<List<CreditCardStatement>> GetOverdueStatements(int creditCardId);

        /// <summary>
        /// Creates a new statement
        /// </summary>
        Task<int> InsertStatement(CreditCardStatement statement);

        /// <summary>
        /// Updates a statement
        /// </summary>
        Task<bool> UpdateStatement(CreditCardStatement statement);

        /// <summary>
        /// Generates a new statement for billing cycle
        /// </summary>
        Task<int> GenerateStatement(int creditCardId, DateTime billingCycleEndDate);

        /// <summary>
        /// Gets all statements (admin only)
        /// </summary>
        Task<List<CreditCardStatement>> GetAllStatements();

        /// <summary>
        /// Gets statements with due dates approaching
        /// </summary>
        Task<List<CreditCardStatement>> GetUpcomingDueStatements(int daysFromNow);

        /// <summary>
        /// Deletes a statement
        /// </summary>
        Task<bool> DeleteStatement(int statementId);

        /// <summary>
        /// Gets statement details for a statement
        /// </summary>
        Task<List<CreditCardStatementDetail>> GetStatementDetails(int statementId);
    }
}
