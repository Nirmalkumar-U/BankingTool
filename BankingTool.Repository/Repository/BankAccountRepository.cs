using BankingTool.Model;
using BankingTool.Model.Dto.BankAccount;
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
        public async Task<List<DropDownDto>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId)
        {
            var aa = await (from a in dataContext.Account.AsNoTracking()
                            where a.CustomerId == customerId && a.AccountTypeId == accountTypeId
                            select a.BankId).ToListAsync();

            return await dataContext.Bank.Where(x => !aa.Contains(x.BankId)).Select(z => new DropDownDto
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
        public async Task<bool> IsCustomerHasCreditCardInThatBank(int customerId, int bankId)
        {
            return await (from a in dataContext.Account.AsNoTracking()
                          join c in dataContext.Card.AsNoTracking() on a.AccountId equals c.AccountId
                          where a.CustomerId == customerId && a.BankId == bankId && c.CardType == CardType.CreditCard
                          select c).AnyAsync();
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
        public async Task<List<TransactionsListDto>> GetTransactionByAccountId(int accountId)
        {
            return await dataContext.Transaction.Where(x => x.AccountId == accountId).Select(z => new TransactionsListDto
            {
                Amount = z.Amount,
                StageBalance = z.StageBalance,
                TransactionDate = z.TransactionTime,
                Description = z.Description,
                TransactionType = z.TransactionType,
                TransactionId = z.TransactionId
            }).ToListAsync();
        }
        public async Task<TransactionsListAccountInfoDto> GetAccountInfo(int accountId, string accountType, string name, string bankName)
        {
            return await dataContext.Account.Where(x => x.AccountId == accountId).Select(z => new TransactionsListAccountInfoDto
            {
                AccountHolderName = name,
                AccountNumber = z.AccountNumber,
                AccountType = accountType,
                Balance = z.Balance,
                BankName = bankName
            }).FirstOrDefaultAsync();
        }
        public async Task<List<TransactionsListCardInfoDto>> GetCardInfoByAccountId(int accountId, string name, string bankName)
        {
            return await dataContext.Card.Where(x => x.AccountId == accountId).Select(z => new TransactionsListCardInfoDto
            {
                BankName = bankName,
                BalanceLimit = z.CardType == CardType.CreditCard ? z.CardLimit : null,
                CardType = z.CardType,
                CardNumber = z.CardNumber,
                CVV = z.CVV,
                ExpirationDate = z.ExpireDate,
                HolderName = name
            }).ToListAsync();
        }
        public async Task<(int, string, string, string)> GetAccountIdByBankIdAndAccountTypeAndCustomerId(int bankId, int accountTypeId, int customerId)
        {
            var accType = await dataContext.CodeValue.FirstOrDefaultAsync(x => x.CodeValueId == accountTypeId);
            var account = await dataContext.Account.FirstOrDefaultAsync(x => x.BankId == bankId && x.AccountTypeId == accType.CodeValueId && x.CustomerId == customerId);
            var name = await (from u in dataContext.Users.AsNoTracking()
                              join c in dataContext.Customer.AsNoTracking() on u.UserId equals c.UserId
                              where c.CustomerId == customerId
                              select u).FirstOrDefaultAsync();
            var bankName = await dataContext.Bank.FirstOrDefaultAsync(x => x.BankId == bankId);
            return (account.AccountId, name.FirstName + " " + name.LastName, bankName.BankName, accType.CodeValue1);
        }
        public async Task<List<DropDownDto>> GetBankDropDownListByCustomerId(int customerId)
        {
            return await (from a in dataContext.Account.AsNoTracking()
                          join b in dataContext.Bank.AsNoTracking() on a.BankId equals b.BankId
                          where a.CustomerId == customerId
                          select new DropDownDto
                          {
                              Key = b.BankId,
                              Value = b.BankName
                          }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int BankId)
        {
            return await (from a in dataContext.Account.AsNoTracking()
                          join cv in dataContext.CodeValue.AsNoTracking() on a.AccountTypeId equals cv.CodeValueId
                          where a.CustomerId == customerId && a.BankId == BankId
                          select new DropDownDto
                          {
                              Key = cv.CodeValueId,
                              Value = cv.CodeValue1
                          }).ToListAsync();
        }

        public async Task<List<DropDownDto>> GetFromAccountListByCustomerId(int customerId)
        {
            return await (from a in dataContext.Account
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join cv in dataContext.CodeValue on a.AccountTypeId equals cv.CodeValueId
                          where a.CustomerId == customerId && a.AccountStatus == AccountStatus.Active
                          select new DropDownDto
                          {
                              Key = a.AccountId,
                              Value = u.FirstName + "/" + b.BankAbbrivation + "/" + a.AccountNumber.Substring(0, 4) + "/" + cv.CodeValue1
                          }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetToAccountListOnWithoutCustomerId(int customerId)
        {
            return await (from a in dataContext.Account
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join cv in dataContext.CodeValue on a.AccountTypeId equals cv.CodeValueId
                          where a.CustomerId != customerId && a.AccountStatus == AccountStatus.Active
                          select new DropDownDto
                          {
                              Key = a.AccountId,
                              Value = u.FirstName + "/" + b.BankAbbrivation + "/" + a.AccountNumber.Substring(0, 4) + "/" + cv.CodeValue1
                          }).ToListAsync();
        }

        public bool CreateAccount(Account account, Transaction transaction, Card debitCard, Card creditCard, CreditScore cardScore, Customer customer,
            bool CustomerWantCreditCard, bool IsAnyAccountForThisCustomer, bool IsUpdatePrimaryAccount)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                int? accountId = InsertAccount(account);
                transaction.AccountId = accountId.Value;
                int? transactionId = InsertTransaction(transaction);
                debitCard.AccountId = accountId.Value;
                InsertCard(debitCard);
                if (IsUpdatePrimaryAccount)
                {
                    UpdateCustomer(customer);
                }
                if (CustomerWantCreditCard)
                {
                    creditCard.AccountId = accountId.Value;
                    InsertCard(creditCard);
                }
                if (IsAnyAccountForThisCustomer)
                {
                    InsertCreditScore(cardScore);
                }
                sqlTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return false;
            }
        }
        public int? InsertAccount(Account account)
        {
            account.CreatedBy = "Admin";
            account.CreatedDate = DateTime.Now;
            account.ModifiedBy = null;
            account.ModifiedDate = null;
            account.IsDeleted = false;
            var isInserted = Insert(account);
            return isInserted ? account.AccountId : null;
        }
        public int? InsertTransaction(Transaction transaction)
        {
            transaction.CreatedBy = "Admin";
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedBy = null;
            transaction.ModifiedDate = null;
            transaction.IsDeleted = false;
            var isInserted = Insert(transaction);
            return isInserted ? transaction.TransactionId : null;
        }
        public int? InsertCard(Card card)
        {
            card.CreatedBy = "Admin";
            card.CreatedDate = DateTime.Now;
            card.ModifiedBy = null;
            card.ModifiedDate = null;
            card.IsDeleted = false;
            var isInserted = Insert(card, true);
            return isInserted ? card.CardId : null;
        }
        public int? InsertCreditScore(CreditScore creditScore)
        {
            creditScore.CreatedBy = "Admin";
            creditScore.CreatedDate = DateTime.Now;
            creditScore.ModifiedBy = null;
            creditScore.ModifiedDate = null;
            creditScore.IsDeleted = false;
            var isInserted = Insert(creditScore);
            return isInserted ? creditScore.CreditScoreId : null;
        }
        public void UpdateCustomer(Customer customer)
        {
            customer.ModifiedBy = "Admin";
            customer.ModifiedDate = DateTime.Now;
            Update(customer);
        }
    }
}
