using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto.CreditCard;

namespace BankingTool.Service.IService
{
    /// <summary>
    /// Service interface for credit card statement operations
    /// </summary>
    public interface ICreditCardStatementService
    {
        /// <summary>
        /// Generates a new statement for a billing cycle
        /// </summary>
        Task<ResponseDto<StatementDTO>> GenerateStatement(int creditCardId, DateTime billingCycleEndDate);

        /// <summary>
        /// Calculates the minimum due amount for a statement
        /// </summary>
        decimal CalculateMinimumDue(decimal balance);

        /// <summary>
        /// Calculates the interest charge for a billing cycle
        /// </summary>
        decimal CalculateInterest(decimal balance, decimal monthlyInterestRate);

        /// <summary>
        /// Gets details of a specific statement
        /// </summary>
        Task<ResponseDto<StatementDTO>> GetStatementDetails(int statementId);

        /// <summary>
        /// Gets statement details with transaction breakdown
        /// </summary>
        Task<ResponseDto<StatementDTO>> GetStatementWithDetails(int statementId);

        /// <summary>
        /// Gets statement history for a card
        /// </summary>
        Task<ResponseDto<List<StatementDTO>>> GetStatementHistory(int creditCardId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Gets the current/latest statement for a card
        /// </summary>
        Task<ResponseDto<StatementDTO>> GetCurrentStatement(int creditCardId);

        /// <summary>
        /// Gets statements by status
        /// </summary>
        Task<ResponseDto<List<StatementDTO>>> GetStatementsByStatus(int creditCardId, StatementStatus status);

        /// <summary>
        /// Gets overdue statements
        /// </summary>
        Task<ResponseDto<List<StatementDTO>>> GetOverdueStatements(int creditCardId);

        /// <summary>
        /// Marks a statement as paid
        /// </summary>
        Task<ResponseDto<bool>> MarkStatementAsPaid(int statementId);

        /// <summary>
        /// Marks a statement as partially paid
        /// </summary>
        Task<ResponseDto<bool>> MarkStatementAsPartiallyPaid(int statementId);

        /// <summary>
        /// Marks a statement as overdue
        /// </summary>
        Task<ResponseDto<bool>> MarkStatementAsOverdue(int statementId);
    }
}
