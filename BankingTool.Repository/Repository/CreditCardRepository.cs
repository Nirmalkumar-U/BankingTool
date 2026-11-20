using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardRepository(DataContext dataContext) : EntityRepository<CreditCard>(dataContext), ICreditCardRepository
    {
        public async Task<CreditCard> GetCreditCardById(int creditCardId)
        {
            return await dataContext.CreditCard
                .AsNoTracking()
                .Include(x => x.Account)
                .ThenInclude(account => account.Bank)
                .FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
        }

        public async Task<List<CreditCard>> GetCreditCardsByCustomerId(int customerId)
        {
            return await dataContext.CreditCard
                .AsNoTracking()
                .Where(x => x.AccountId == customerId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<CreditCard>> GetActiveCreditCardsByCustomerId()
        {
            var accountIds = await dataContext.Account.Where(x => x.CustomerId == dataContext.CustomerId).Select(z=>z.AccountId).ToListAsync();
            return await dataContext.CreditCard
                .AsNoTracking()
                .Include(x => x.Account)
                .ThenInclude(account => account.Bank)
                .Where(x => accountIds.Contains(x.AccountId) && x.CardStatus == CreditCardStatus.Active && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<CreditCard> GetCreditCardByCardNumber(string cardNumber)
        {
            return await dataContext.CreditCard
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
        }

        public async Task<int> InsertCreditCard(CreditCard creditCard)
        {
            creditCard.CreatedDate = DateTime.UtcNow;
            creditCard.ModifiedDate = DateTime.UtcNow;
            creditCard.CardHolderName = dataContext.UserName;
            creditCard.CreatedBy = dataContext.UserName;
            creditCard.ApprovedBy = dataContext.UserName;

            dataContext.CreditCard.Add(creditCard);
            await dataContext.SaveChangesAsync();
            return creditCard.CreditCardId;
        }

        public async Task<bool> UpdateCreditCard(CreditCard creditCard)
        {
            creditCard.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCard.Update(creditCard);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> CloseCreditCard(int creditCardId, string reason)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return false;

            card.CardStatus = CreditCardStatus.Closed;
            card.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCard.Update(card);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCreditLimit(int creditCardId, decimal newLimit)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return false;

            // limitUsed is the amount already used by the customer, AvailableCredit Total limit customer can use, CreditLimit is customer can set a limit for the card. So, New limit should be greater than or equal to limitUsed. If new limit is less than limitUsed, then we cannot update the limit.
            if (newLimit < card.LimitUsed)
            {
                return false;
            }
            var limitUserHas = card.AvailableCredit - card.LimitUsed;
            
            if (newLimit > limitUserHas)
            {
                return false;
            }

            card.CreditLimit = newLimit;
            card.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCard.Update(card);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> BlockCreditCard(int creditCardId, string reason)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return false;

            card.CardStatus = CreditCardStatus.Blocked;
            card.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCard.Update(card);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnblockCreditCard(int creditCardId)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return false;

            card.CardStatus = CreditCardStatus.Active;
            card.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCard.Update(card);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<CreditCard>> GetAllCreditCards()
        {
            return await dataContext.CreditCard
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteCreditCard(int creditCardId)
        {
            var card = await dataContext.CreditCard.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
            if (card == null) return false;

            dataContext.CreditCard.Remove(card);
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
