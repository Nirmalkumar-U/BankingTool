using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardStatementRepository(DataContext dataContext) : EntityRepository<CreditCardStatement>(dataContext), ICreditCardStatementRepository
    {
        public async Task<CreditCardStatement> GetStatementById(int statementId)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StatementId == statementId);
        }

        public async Task<List<CreditCardStatement>> GetStatementsByCardId(int creditCardId)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.StatementDate)
                .ToListAsync();
        }

        public async Task<CreditCardStatement> GetCurrentStatement(int creditCardId)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.StatementDate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CreditCardStatementDetail>> GetStatementDetails(int statementId)
        {
            return await dataContext.CreditCardStatementDetail
                .AsNoTracking()
                .Where(x => x.StatementId == statementId)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }
    }
}
