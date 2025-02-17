using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class BankAccountRepository(DataContext dataContext) : EntityRepository<Account>(dataContext), IBankAccountRepository
    {
        public async Task<List<DropDownDto>> GetBankDetailsDropDown()
        {
            return await dataContext.Bank.Select(z => new DropDownDto
            {
                Key = z.BankId,
                Value = z.BankAbbrivation + " | " + z.BankName,
            }).OrderBy(x => x.Value).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetAccountTypeDropDown()
        {
            return await dataContext.CodeValue.Where(x => x.TypeCode == Constants.AccountType && x.InUse).OrderBy(s => s.Sequence)
                .Select(z => new DropDownDto
                {
                    Key = z.CodeValueId,
                    Value = z.CodeValue1,
                }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetAllActiveCustomerDropDown()
        {
            return await (from c in dataContext.Customer.AsNoTracking()
                          join u in dataContext.Users.AsNoTracking() on c.UserId equals u.UserId
                          where u.IsActive
                          select new DropDownDto
                          {
                              Key = c.CustomerId,
                              Value = u.FirstName + " " + u.LastName,
                          }).ToListAsync();
        }
        public async Task<Account> GetLastAccount()
        {
            if (dataContext.Account.Any())
                return await dataContext.Account.LastOrDefaultAsync();
            return null;
        }
        public async Task<Card> GetLastCard(string cardType)
        {
            if (dataContext.Card.Any())
                return await dataContext.Card.Where(x => x.CardType == cardType).OrderBy(z => z.CardId).LastOrDefaultAsync();
            return null;
        }
        public async Task<bool> IsAnyAccountForThisCustomer(int customerId)
        {
            return await dataContext.Account.AnyAsync(x => x.CustomerId == customerId);
        }
        public async Task<Customer> GetCustomerByCustomerId(int customerId)
        {
            return await dataContext.Customer.FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public int? InsertAccount(Account account)
        {
            account.CreatedBy = "Admin";
            account.CreatedDate = DateTime.Now;
            account.ModifiedBy = null;
            account.ModifiedDate = null;
            account.IsDeleted = false;
            var isInserted = Insert(account, false);
            return isInserted ? account.AccountId : null;
        }
        public int? InsertTransaction(Transaction transaction)
        {
            transaction.CreatedBy = "Admin";
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedBy = null;
            transaction.ModifiedDate = null;
            transaction.IsDeleted = false;
            var isInserted = Insert(transaction, false);
            return isInserted ? transaction.TransactionId : null;
        }
        public int? InsertCard(Card card)
        {
            card.CreatedBy = "Admin";
            card.CreatedDate = DateTime.Now;
            card.ModifiedBy = null;
            card.ModifiedDate = null;
            card.IsDeleted = false;
            var isInserted = Insert(card, false);
            return isInserted ? card.CardId : null;
        }
        public int? InsertCreditScore(CreditScore creditScore)
        {
            creditScore.CreatedBy = "Admin";
            creditScore.CreatedDate = DateTime.Now;
            creditScore.ModifiedBy = null;
            creditScore.ModifiedDate = null;
            creditScore.IsDeleted = false;
            var isInserted = Insert(creditScore, false);
            return isInserted ? creditScore.CreditScoreId : null;
        }
        public void UpdateCustomer(Customer customer)
        {
            customer.ModifiedBy = "Admin";
            customer.ModifiedDate = DateTime.Now;
            Update(customer, false);
        }
    }
}
