using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardTransactionRepository(DataContext dataContext) : EntityRepository<CreditCardTransaction>(dataContext), ICreditCardTransactionRepository
    {
        public async Task<CreditCardTransaction> GetTransactionById(int transactionId)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TransactionId == transactionId);
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByCardId(int creditCardId)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.TransactionDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.TransactionDate >= fromDate && x.TransactionDate <= toDate)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByType(int creditCardId, TransactionType transactionType)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.TransactionType == transactionType)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByStatus(int creditCardId, TransactionStatus status)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.TransactionStatus == status)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<int> InsertTransaction(CreditCardTransaction transaction)
        {
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardTransaction.Add(transaction);
            await dataContext.SaveChangesAsync();
            return transaction.TransactionId;
        }

        public async Task<bool> UpdateTransaction(CreditCardTransaction transaction)
        {
            transaction.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardTransaction.Update(transaction);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReverseTransaction(int transactionId, string reason)
        {
            var transaction = await dataContext.CreditCardTransaction.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (transaction == null) return false;

            transaction.TransactionStatus = TransactionStatus.Reversed;
            transaction.Description = $"{transaction.Description} - Reversed: {reason}";
            transaction.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardTransaction.Update(transaction);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<decimal> CalculateTotalCharges(int creditCardId, DateTime startDate, DateTime endDate)
        {
            return await dataContext.CreditCardTransaction
                .Where(x => x.CreditCardId == creditCardId 
                    && x.TransactionDate >= startDate 
                    && x.TransactionDate <= endDate 
                    && x.TransactionType == TransactionType.Purchase
                    && x.TransactionStatus == TransactionStatus.Completed)
                .SumAsync(x => x.Amount);
        }

        public async Task<decimal> CalculateTotalPayments(int creditCardId, DateTime startDate, DateTime endDate)
        {
            return await dataContext.CreditCardTransaction
                .Where(x => x.CreditCardId == creditCardId 
                    && x.TransactionDate >= startDate 
                    && x.TransactionDate <= endDate 
                    && x.TransactionType == TransactionType.Payment
                    && x.TransactionStatus == TransactionStatus.Completed)
                .SumAsync(x => x.Amount);
        }

        public async Task<List<CreditCardTransaction>> GetAllTransactions()
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetRecentTransactions(int creditCardId, int count)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.TransactionDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<bool> DeleteTransaction(int transactionId)
        {
            var transaction = await dataContext.CreditCardTransaction.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (transaction == null) return false;

            dataContext.CreditCardTransaction.Remove(transaction);
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
