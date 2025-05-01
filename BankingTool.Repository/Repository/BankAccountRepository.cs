using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Model;
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
                return await dataContext.Account.OrderByDescending(a => a.AccountId).FirstOrDefaultAsync();
            return null;
        }
        public async Task<Card> GetLastCard(string cardType)
        {
            if (dataContext.Card.Any())
                return await dataContext.Card.Where(x => x.CardType == cardType).OrderByDescending(z => z.CardId).FirstOrDefaultAsync();
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
        public async Task<List<GetTransactionsListResponseTransactionList>> GetTransactionByAccountId(int accountId)
        {
            var transactions = await (
                    from tp in dataContext.TransactionDetail
                    join t in dataContext.Transaction on tp.TransactionId equals t.TransactionId
                    join tpOtherJoin in dataContext.TransactionDetail on t.TransactionId equals tpOtherJoin.TransactionId into tpOtherGroup
                    from tpOther in tpOtherGroup.DefaultIfEmpty()
                    join a in dataContext.Account on tp.AccountId equals a.AccountId
                    join aOtherJoin in dataContext.Account on tpOther.AccountId equals aOtherJoin.AccountId into aOtherGroup
                    from aOther in aOtherGroup.DefaultIfEmpty()
                    where tp.AccountId == accountId
                    select new GetTransactionsListResponseTransactionList
                    {
                        TransactionId = t.TransactionId,
                        Amount = t.Amount,
                        StageBalance = tp.StageBalance,
                        TransactionDate = t.TransactionTime,
                        Description = t.Description,
                        TransactionType = tp.TransactionType,
                        TransactionCategory = t.TransactionCategory,

                        FromAccountId = t.TransactionCategory == TransactionCatagory.Transfer && tp.TransactionRole == "Receiver" ? aOther.AccountNumber :
                                        t.TransactionCategory == TransactionCatagory.Transfer && tp.TransactionRole == "Sender" ? a.AccountNumber :
                                        t.TransactionCategory == TransactionCatagory.Withdraw || t.TransactionCategory == TransactionCatagory.Deposit ? a.AccountNumber : null,

                        ToAccountId = t.TransactionCategory == TransactionCatagory.Transfer && tp.TransactionRole == "Sender" ? aOther.AccountNumber :
                                      t.TransactionCategory == TransactionCatagory.Transfer && tp.TransactionRole == "Receiver" ? a.AccountNumber : null
                    }
                ).OrderByDescending(x => x.TransactionDate).ToListAsync();


            return transactions;
        }


        public async Task<GetTransactionsListResponseAccountInfo> GetAccountInfo(int accountId, string accountType, string name, string bankName)
        {
            return await dataContext.Account.Where(x => x.AccountId == accountId).Select(z => new GetTransactionsListResponseAccountInfo
            {
                AccountHolderName = name,
                AccountNumber = z.AccountNumber,
                AccountType = accountType,
                Balance = z.Balance,
                BankName = bankName
            }).FirstOrDefaultAsync();
        }
        public async Task<List<GetTransactionsListResponseCardInfo>> GetCardInfoByAccountId(int accountId, string name, string bankName)
        {
            return await dataContext.Card.Where(x => x.AccountId == accountId).Select(z => new GetTransactionsListResponseCardInfo
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
        public async Task<List<DropDownDto>> GetBankDropDownListByCustomerId()
        {
            return await (from a in dataContext.Account.AsNoTracking()
                          join b in dataContext.Bank.AsNoTracking() on a.BankId equals b.BankId
                          where a.CustomerId == dataContext.CustomerId
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

        public async Task<List<DropDownDto>> GetFromAccountListByCustomerId()
        {
            return await (from a in dataContext.Account
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join cv in dataContext.CodeValue on a.AccountTypeId equals cv.CodeValueId
                          where a.CustomerId == dataContext.CustomerId && a.AccountStatus == AccountStatus.Active
                          select new DropDownDto
                          {
                              Key = a.AccountId,
                              Value = u.FirstName + "/" + b.BankAbbrivation + "/" + a.AccountNumber.Substring(0, 4) + "/" + cv.CodeValue1
                          }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetToAccountListOnWithoutCustomerId()
        {
            return await (from a in dataContext.Account
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join cv in dataContext.CodeValue on a.AccountTypeId equals cv.CodeValueId
                          where a.CustomerId != dataContext.CustomerId && a.AccountStatus == AccountStatus.Active
                          select new DropDownDto
                          {
                              Key = a.AccountId,
                              Value = u.FirstName + "/" + b.BankAbbrivation + "/" + a.AccountNumber.Substring(0, 4) + "/" + cv.CodeValue1
                          }).ToListAsync();
        }

        public async Task<int> GetAccountBalance(int accountId)
        {
            return await (from a in dataContext.Account
                          where a.AccountId != accountId && a.AccountStatus == AccountStatus.Active
                          select a.Balance).FirstOrDefaultAsync();
        }

        public bool CreateAccount(Account account, Transaction transaction, TransactionDetail transactionDetail, Card debitCard, Card creditCard, CreditScore cardScore, Customer customer,
            bool CustomerWantCreditCard, bool IsAnyAccountForThisCustomer, bool IsUpdatePrimaryAccount)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                int? accountId = InsertAccount(account);
                int? transactionId = InsertTransaction(transaction);
                transactionDetail.AccountId = accountId.Value;
                transactionDetail.TransactionId = transactionId.Value;
                InsertTransactionDetail(transactionDetail);
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

        public bool TransferAmount(Account fromAccount, Account toAccount, Transaction fromTransaction, Transaction toTransaction, TransactionDetail fromTransactionDetail,
            TransactionDetail toTransactionDetail)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                UpdateAccount(fromAccount);
                UpdateAccount(toAccount);
                var fromTransactionId = InsertTransaction(fromTransaction);
                var toTransactionId = InsertTransaction(toTransaction);
                fromTransactionDetail.TransactionId = fromTransactionId.Value;
                toTransactionDetail.TransactionId = toTransactionId.Value;
                InsertTransactionDetail(toTransactionDetail);
                InsertTransactionDetail(fromTransactionDetail);
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
        public async Task<Account> GetAccount(int AccountId)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.AccountId == AccountId);
        }
        public async Task<Transaction> GetTransaction(int TransactionId)
        {
            return await dataContext.Transaction.FirstOrDefaultAsync(x => x.TransactionId == TransactionId);
        }

        public int? InsertAccount(Account account)
        {
            account.CreatedBy = dataContext.UserEmail;
            account.CreatedDate = DateTime.Now;
            account.ModifiedBy = null;
            account.ModifiedDate = null;
            account.IsDeleted = false;
            var isInserted = Insert(account);
            return isInserted ? account.AccountId : null;
        }
        public void UpdateAccount(Account account)
        {
            account.ModifiedBy = dataContext.UserEmail;
            account.ModifiedDate = DateTime.Now;
            Update(account);
        }
        public int? InsertTransaction(Transaction transaction)
        {
            transaction.CreatedBy = dataContext.UserEmail;
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedBy = null;
            transaction.ModifiedDate = null;
            transaction.IsDeleted = false;
            var isInserted = Insert(transaction);
            return isInserted ? transaction.TransactionId : null;
        }
        public void InsertTransactionDetail(TransactionDetail transactionDetail)
        {
            var isInserted = Insert(transactionDetail);
        }
        public int? InsertCard(Card card)
        {
            card.CreatedBy = dataContext.UserEmail;
            card.CreatedDate = DateTime.Now;
            card.ModifiedBy = null;
            card.ModifiedDate = null;
            card.IsDeleted = false;
            var isInserted = Insert(card, true);
            return isInserted ? card.CardId : null;
        }
        public int? InsertCreditScore(CreditScore creditScore)
        {
            creditScore.CreatedBy = dataContext.UserEmail;
            creditScore.CreatedDate = DateTime.Now;
            creditScore.ModifiedBy = null;
            creditScore.ModifiedDate = null;
            creditScore.IsDeleted = false;
            var isInserted = Insert(creditScore);
            return isInserted ? creditScore.CreditScoreId : null;
        }
        public void UpdateCustomer(Customer customer)
        {
            customer.ModifiedBy = dataContext.UserEmail;
            customer.ModifiedDate = DateTime.Now;
            Update(customer);
        }
    }
}
