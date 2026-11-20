using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
            transaction.CreatedBy = dataContext.UserName;
            transaction.ModifiedBy = dataContext.UserName;
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

        public async Task<List<TransactionDTO>> GetRecentTransactions()
        {
            return await (from cc in dataContext.CreditCard
                          join a in dataContext.Account on cc.AccountId equals a.AccountId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join cct in dataContext.CreditCardTransaction on cc.CreditCardId equals cct.CreditCardId
                          where c.CustomerId == dataContext.CustomerId
                          orderby cct.TransactionDate descending
                          select new TransactionDTO
                          {
                              TransactionId = cct.TransactionId,
                              TransactionDate = cct.TransactionDate,
                              TransactionType = cct.TransactionType.EnumToString(),
                              Amount = cct.Amount,
                              Description = cct.Description,
                              MerchantName = cct.MerchantName,
                              TransactionStatus = cct.TransactionStatus.EnumToString(),
                              ReferenceNumber = cct.ReferenceNumber,
                              CreditCardId = cc.CreditCardId,
                              CreditCardNumber = cc.CardNumber,
                              AccountId = a.AccountId,
                              CardDetail = b.BankAbbrivation + " | " + cc.CardNumber.Substring(cc.CardNumber.Length - 4),
                              CardholderName = cc.CardHolderName
                          }).ToListAsync();
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
