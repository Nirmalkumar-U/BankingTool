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

        public async Task<List<CreditCardStatement>> GetStatementsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.StatementDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<List<CreditCardStatement>> GetStatementsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.StatementDate >= fromDate && x.StatementDate <= toDate)
                .OrderByDescending(x => x.StatementDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardStatement>> GetStatementsByStatus(int creditCardId, StatementStatus status)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.StatementStatus == status)
                .OrderByDescending(x => x.StatementDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardStatement>> GetOverdueStatements(int creditCardId)
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.DueDate < DateTime.UtcNow && x.StatementStatus != StatementStatus.Paid)
                .OrderByDescending(x => x.DueDate)
                .ToListAsync();
        }

        public async Task<int> InsertStatement(CreditCardStatement statement)
        {
            statement.CreatedDate = DateTime.UtcNow;
            statement.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardStatement.Add(statement);
            await dataContext.SaveChangesAsync();
            return statement.StatementId;
        }

        public async Task<bool> UpdateStatement(CreditCardStatement statement)
        {
            statement.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardStatement.Update(statement);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<int> GenerateStatement(int creditCardId, DateTime billingCycleEndDate)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return 0;

            var billingCycleStartDate = billingCycleEndDate.AddMonths(-1).AddDays(1);

            var statement = new CreditCardStatement
            {
                CreditCardId = creditCardId,
                BillingCycleStartDate = billingCycleStartDate,
                BillingCycleEndDate = billingCycleEndDate,
                StatementDate = DateTime.UtcNow,
                TotalCharges = 0,
                TotalPayments = 0,
                Interest = 0,
                Fees = 0,
                PreviousBalance = card.CurrentBalance,
                CardBalance = card.CurrentBalance,
                MinimumDueAmount = card.CurrentBalance * 0.05m,
                DueDate = billingCycleEndDate.AddDays(20),
                StatementStatus = StatementStatus.Generated,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            dataContext.CreditCardStatement.Add(statement);
            await dataContext.SaveChangesAsync();
            return statement.StatementId;
        }

        public async Task<List<CreditCardStatement>> GetAllStatements()
        {
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .OrderByDescending(x => x.StatementDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardStatement>> GetUpcomingDueStatements(int daysFromNow)
        {
            var targetDate = DateTime.UtcNow.AddDays(daysFromNow);
            return await dataContext.CreditCardStatement
                .AsNoTracking()
                .Where(x => x.DueDate >= DateTime.UtcNow && x.DueDate <= targetDate && x.StatementStatus != StatementStatus.Paid)
                .OrderBy(x => x.DueDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteStatement(int statementId)
        {
            var statement = await dataContext.CreditCardStatement.FirstOrDefaultAsync(x => x.StatementId == statementId);
            if (statement == null) return false;

            dataContext.CreditCardStatement.Remove(statement);
            return await dataContext.SaveChangesAsync() > 0;
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
