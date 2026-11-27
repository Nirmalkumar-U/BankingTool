using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto.CreditCard;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardTransactionRepository(DataContext dataContext) : EntityRepository<CreditCardTransaction>(dataContext), ICreditCardTransactionRepository
    {
        public async Task<List<CreditCardTransaction>> GetTransactionsByCardId(int creditCardId)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardTransaction>> GetTransactionsByDateRange(int creditCardId, DateTime fromDate, DateTime toDate)
        {
            return await dataContext.CreditCardTransaction
                .AsNoTracking()
                .Where(x => x.CreditCardId == creditCardId && x.TransactionDate >= fromDate && x.TransactionDate.Date <= toDate.Date)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<(bool Success, int TransactionId)> SaveCreditCardTransaction(CreditCardTransaction transaction, CreditCard creditCard, List<BudgetTransactionDto> budgetTransactions)
        {
            using var sqlTransaction = await dataContext.Database.BeginTransactionAsync();

            try
            {
                transaction.CreatedDate = DateTime.UtcNow;
                transaction.ModifiedDate = DateTime.UtcNow;
                transaction.CreatedBy = dataContext.UserName;
                transaction.ModifiedBy = dataContext.UserName;

                creditCard.ModifiedDate = DateTime.UtcNow;
                creditCard.ModifiedBy = dataContext.UserName;

                dataContext.CreditCardTransaction.Add(transaction);
                dataContext.CreditCard.Update(creditCard);

                foreach (var budget in budgetTransactions)
                {
                    budget.BudgetAccount.ModifiedDate = DateTime.Now;
                    budget.BudgetAccount.ModifiedBy = dataContext.UserName;
                    dataContext.BudgetAccount.Update(budget.BudgetAccount);
                    budget.BudgetTransaction.CreatedBy = dataContext.UserName;
                    budget.BudgetTransaction.CreatedDate = DateTime.Now;
                    dataContext.BudgetTransaction.Add(budget.BudgetTransaction);
                }

                await dataContext.SaveChangesAsync();

                foreach (var budget in budgetTransactions)
                {
                    budget.BudgetTransactionDetail.BudgetTransactionId = budget.BudgetTransaction.BudgetTransactionId;
                    dataContext.BudgetTransactionDetail.Add(budget.BudgetTransactionDetail);
                }

                if (budgetTransactions.Count > 0)
                    await dataContext.SaveChangesAsync();

                await sqlTransaction.CommitAsync();
                return (true, transaction.TransactionId);
            }
            catch
            {
                await sqlTransaction.RollbackAsync();
                return (false, 0);
            }
        }

        public async Task<bool> ProcessPaymentWithStatement(CreditCardTransaction transaction, CreditCard creditCard, CreditCardStatement statement, List<CreditCardTransaction> transactions)
        {
            using var sqlTransaction = await dataContext.Database.BeginTransactionAsync();

            try
            {
                transaction.CreatedDate = DateTime.UtcNow;
                transaction.ModifiedDate = DateTime.UtcNow;
                transaction.CreatedBy = dataContext.UserName;
                transaction.ModifiedBy = dataContext.UserName;

                creditCard.ModifiedDate = DateTime.UtcNow;
                creditCard.ModifiedBy = dataContext.UserName;

                statement.CreatedDate = DateTime.UtcNow;
                statement.ModifiedDate = DateTime.UtcNow;
                statement.CreatedBy = dataContext.UserName;
                statement.ModifiedBy = dataContext.UserName;

                dataContext.CreditCardTransaction.Add(transaction);
                dataContext.CreditCard.Update(creditCard);
                await dataContext.SaveChangesAsync();

                dataContext.CreditCardStatement.Add(statement);
                await dataContext.SaveChangesAsync();

                foreach (var trans in transactions)
                {
                    dataContext.CreditCardStatementDetail.Add(new CreditCardStatementDetail
                    {
                        StatementId = statement.StatementId,
                        TransactionId = trans.TransactionId,
                        TransactionDate = trans.TransactionDate,
                        Description = trans.Description,
                        Amount = trans.Amount,
                        TransactionType = trans.TransactionType.EnumToString(),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        CreatedBy = dataContext.UserName,
                        ModifiedBy = dataContext.UserName
                    });
                }

                if (transactions.Count > 0)
                    await dataContext.SaveChangesAsync();

                await sqlTransaction.CommitAsync();
                return true;
            }
            catch
            {
                await sqlTransaction.RollbackAsync();
                return false;
            }
        }

        public async Task<List<TransactionResponse>> GetAllTransactionsOfCustomer()
        {
            return await (from cc in dataContext.CreditCard
                          join a in dataContext.Account on cc.AccountId equals a.AccountId
                          join b in dataContext.Bank on a.BankId equals b.BankId
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join cct in dataContext.CreditCardTransaction on cc.CreditCardId equals cct.CreditCardId
                          where c.CustomerId == dataContext.CustomerId
                          orderby cct.TransactionDate descending
                          select new TransactionResponse
                          {
                              Transaction = new TransactionResponseTransaction
                              {
                                  Id = cct.TransactionId,
                                  TransactionDate = cct.TransactionDate,
                                  TransactionType = cct.TransactionType.EnumToString(),
                                  Amount = cct.Amount,
                                  Description = cct.Description,
                                  MerchantName = cct.MerchantName,
                                  TransactionStatus = cct.TransactionStatus.EnumToString(),
                                  ReferenceNumber = cct.ReferenceNumber
                              },

                              CreditCard = new TransactionResponseCreditCard
                              {
                                  Id = cc.CreditCardId,
                                  CardDetail = b.BankAbbrivation + " | " + cc.CardNumber.Substring(cc.CardNumber.Length - 4),
                                  CardholderName = cc.CardHolderName,
                                  CreditCardNumber = cc.CardNumber
                              },
                              Account = new TransactionResponseAccount
                              {
                                  Id = a.AccountId,
                              }
                          }).ToListAsync();
        }
    }
}
