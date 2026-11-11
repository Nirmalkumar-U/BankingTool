using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardFeeRepository(DataContext dataContext) : EntityRepository<CreditCardFee>(dataContext), ICreditCardFeeRepository
    {
        public async Task<CreditCardFee> GetFeeById(int feeId)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FeeId == feeId);
        }

        public async Task<List<CreditCardFee>> GetFeesByCardId(int creditCardId)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardFee>> GetFeesByCardIdWithPagination(int creditCardId, int pageNumber, int pageSize)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.EffectiveDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<CreditCardFee>> GetFeesByType(int creditCardId, string feeType)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.FeeType == feeType)
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardFee>> GetFeesByDateRange(int creditCardId, DateTime fromDate, DateTime toDate)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.EffectiveDate >= fromDate && x.EffectiveDate <= toDate)
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardFee>> GetFeesByBillingCycle(int creditCardId, DateTime cycleStartDate, DateTime cycleEndDate)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.EffectiveDate >= cycleStartDate && x.EffectiveDate <= cycleEndDate)
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardFee>> GetRecentFees(int creditCardId, int count)
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.EffectiveDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> InsertFee(CreditCardFee fee)
        {
            fee.CreatedDate = DateTime.UtcNow;
            fee.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardFee.Add(fee);
            await dataContext.SaveChangesAsync();
            return fee.FeeId;
        }

        public async Task<bool> UpdateFee(CreditCardFee fee)
        {
            fee.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardFee.Update(fee);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<decimal> CalculateTotalFees(int creditCardId, DateTime startDate, DateTime endDate)
        {
            return await dataContext.CreditCardFee
                .Where(x => x.CreditCardId == creditCardId && x.EffectiveDate >= startDate && x.EffectiveDate <= endDate)
                .SumAsync(x => x.Amount);
        }

        public async Task<List<CreditCardFee>> GetAllFees()
        {
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteFee(int feeId)
        {
            var fee = await dataContext.CreditCardFee.FirstOrDefaultAsync(x => x.FeeId == feeId);
            if (fee == null) return false;

            dataContext.CreditCardFee.Remove(fee);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<CreditCardFee>> GetProjectedFees(int creditCardId)
        {
            // Projected fees are typically based on upcoming fees (e.g., annual fee due, late payment fee if not paid)
            return await dataContext.CreditCardFee
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.EffectiveDate >= DateTime.UtcNow && x.EffectiveDate <= DateTime.UtcNow.AddMonths(1))
                .OrderBy(x => x.EffectiveDate)
                .ToListAsync();
        }
    }
}
