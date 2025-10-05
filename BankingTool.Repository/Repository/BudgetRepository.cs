using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class BudgetRepository(DataContext dataContext) : EntityRepository<BudgetAccount>(dataContext), IBudgetRepository
    {
        public async Task<BudgetAccount> GetLastBudgetAccount()
        {
            if (dataContext.BudgetAccount.Any())
                return await dataContext.BudgetAccount.OrderByDescending(a => a.BudgetAccountId).FirstOrDefaultAsync();
            return null;
        }
        public async Task<bool> CreateBudgetAccount(BudgetAccount budgetAccount, BudgetTransaction budgetTransaction, BudgetTransactionDetail budgetTransactionDetails)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                budgetAccount.CustomerId = dataContext.CustomerId;
                var budgetAccountId = InsertBudgetAccount(budgetAccount);
                var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                budgetTransactionDetails.BudgetTransactionId = budgetTransactionId.Value;
                budgetTransactionDetails.BudgetAccountId = budgetAccountId.Value;
                var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetails);
                sqlTransaction.Commit();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return await Task.FromResult(false);
            }
        }
        public async Task<int> GetPrimaryAccountIdOfCustomer()
        {
            var primaryAccount = await dataContext.Customer.Include(x => x.Account)
                                            .Where(x => x.CustomerId == dataContext.CustomerId
                                                     && x.Account.Any(a => a.AccountNumber == x.PrimaryAccountNumber))
                                            .Select(x => x.Account.FirstOrDefault(a => a.AccountNumber == x.PrimaryAccountNumber))
                                            .FirstOrDefaultAsync();

            return primaryAccount?.AccountId ?? 0;
        }
        public async Task<BudgetAccount> GetBudgetAccountById(int budgetAccountId)
        {
            var budgetAccount = await dataContext.BudgetAccount
                .FirstOrDefaultAsync(x => x.BudgetAccountId == budgetAccountId);
            return budgetAccount;
        }
        public async Task<List<DropDownDto>> GetBudgetAccountDropdown()
        {
            var budgetAccount = await dataContext.BudgetAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.BudgetStatus == BudgetAccountStatusEnum.Active.EnumToString() && x.IsDeleted == false)
                .Select(x => new DropDownDto
                {
                    Key = x.BudgetAccountId,
                    Value = x.BudgetName,
                    IsSelected = false
                }).ToListAsync();
            return budgetAccount;
        }

        public async Task<List<DropDownDto>> GetBudgetAccountDropdownExceptBudgetAccountId(int budgetAccountId)
        {
            var budgetAccount = await dataContext.BudgetAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.BudgetAccountId != budgetAccountId && x.BudgetStatus == BudgetAccountStatusEnum.Active.EnumToString() && x.IsDeleted == false)
                .Select(x => new DropDownDto
                {
                    Key = x.BudgetAccountId,
                    Value = x.BudgetName,
                    IsSelected = false
                }).ToListAsync();
            return budgetAccount;
        }
        public async Task<decimal> GetBudgetAccountBalance(int budgetAccountId)
        {
            var budgetAccount = await dataContext.BudgetAccount
                .FirstOrDefaultAsync(x => x.CustomerId == dataContext.CustomerId && x.BudgetAccountId == budgetAccountId && x.BudgetStatus == BudgetAccountStatusEnum.Active.EnumToString() && x.IsDeleted == false);
            return budgetAccount.Balance;
        }
        public async Task<List<GetBudgetList>> GetBudgetList()
        {
            var budgetAccountList = await dataContext.BudgetAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.IsDeleted == false)
                .Select(x => new GetBudgetList
                {
                    BudgetAccountId = x.BudgetAccountId,
                    BudgetName = x.BudgetName,
                    BudgetNumber = x.BudgetNumber,
                    Balance = x.Balance,
                    BudgetStatus = x.BudgetStatus.BudgetAccountStatusStringToEnum()
                }).ToListAsync();
            return budgetAccountList;
        }
        public async Task<List<GetBudgetTransactionList>> GetBudgetTransactionList(int budgetAccountId)
        {
            var sql = "EXEC GetBudgetAccountTransactions @BudgetAccountId";
            var parameters = new[]
                            {
                                new Microsoft.Data.SqlClient.SqlParameter("@BudgetAccountId", budgetAccountId.ToString())
                            };

            var transactions = await dataContext.Set<GetBudgetTransactionList>()
                    .FromSqlRaw(sql, parameters)
                    .ToListAsync();

            return transactions;
        }
        public async Task<BudgetTransaction> GetLastBudgetTransaction(int BudgetAccountId)
        {
            return await (from t in dataContext.BudgetTransaction
                          join td in dataContext.BudgetTransactionDetail on t.BudgetTransactionId equals td.BudgetTransactionId
                          where td.BudgetAccountId == BudgetAccountId
                          orderby t.TransactionTime descending
                          select t).FirstOrDefaultAsync();
        }
        public async Task<BudgetNotification> GetBudgetNotification(int BudgetNotificationId)
        {
            return await dataContext.BudgetNotification.FirstOrDefaultAsync(x => x.BudgetNotificationId == BudgetNotificationId); ;
        }
        public async Task<bool> AcceptBudgetTransaction(List<AcceptBudgetTransactionRequestBudgetAcccount> BudgetAccount, int BudgetNotificationId)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                var budgetNotification = await dataContext.BudgetNotification.FirstOrDefaultAsync(x => x.BudgetNotificationId == BudgetNotificationId);
                foreach (var item in BudgetAccount)
                {
                    var budgetTransactionAboveTransferedTime = await dataContext.BudgetTransaction
                    .Include(x => x.BudgetTransactionDetail)
                    .Where(x => x.TransactionTime > budgetNotification.TransactionDate &&
                     x.BudgetTransactionDetail.Any(d => d.BudgetAccountId == item.Id))
                    .OrderBy(x => x.TransactionTime)
                    .ToListAsync();
                    if (budgetTransactionAboveTransferedTime == null || budgetTransactionAboveTransferedTime.Count == 0)
                    {
                        var lastBudgetAccountTransaction = await GetLastBudgetTransaction(item.Id);
                        var budgetTransactionDate = budgetNotification.TransactionDate;
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                        {
                            budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                            budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                        }
                        var budgetAccount = await GetBudgetAccountById(item.Id);
                        budgetAccount.Balance = budgetNotification.TransactionRole == TransactionRoleEnum.Receiver.EnumToString() ? budgetAccount.Balance + item.Amount : budgetAccount.Balance - item.Amount;
                        var budgetTransaction = new BudgetTransaction()
                        {
                            Amount = item.Amount,
                            Description = budgetNotification.Description,
                            TransactionTime = budgetTransactionDate,
                            TransactionCategory = budgetNotification.TransactionCatagory,
                            TransactionTag = budgetNotification.TransactionTag
                        };
                        var budgetTransactionDetails = new BudgetTransactionDetail()
                        {
                            TransactionType = budgetNotification.TransactionType,
                            StageBalance = budgetAccount.Balance,
                            TransactionRole = budgetNotification.TransactionRole,
                            AccountId = budgetNotification.AccountId,
                            BudgetAccountId = budgetAccount.BudgetAccountId
                        };
                        UpdateBudgetAccount(budgetAccount);
                        var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                        budgetTransactionDetails.BudgetTransactionId = budgetTransactionId.Value;
                        var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetails);
                    }
                    else
                    {
                        var previousTransaction = dataContext.BudgetTransaction
                            .Include(x => x.BudgetTransactionDetail)
                            .Where(t => t.TransactionTime < budgetNotification.TransactionDate && t.BudgetTransactionDetail.Any(d => d.BudgetAccountId == item.Id))
                            .OrderByDescending(t => t.TransactionTime)
                            .FirstOrDefault();

                        var lastTransactionAmount = previousTransaction.Amount;
                        var lastTransactionStageBalance = previousTransaction.BudgetTransactionDetail.FirstOrDefault().StageBalance;

                        BudgetTransaction budgetTransaction = new BudgetTransaction
                        {
                            TransactionCategory = budgetNotification.TransactionCatagory,
                            Amount = item.Amount,
                            Description = budgetNotification.Description,
                            TransactionTime = budgetNotification.TransactionDate,
                            TransactionTag = budgetNotification.TransactionTag
                        };
                        var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                        BudgetTransactionDetail budgetTransactionDetail = new BudgetTransactionDetail
                        {
                            TransactionType = budgetNotification.TransactionType,
                            StageBalance = budgetNotification.TransactionRole == TransactionRoleEnum.Receiver.EnumToString() ? lastTransactionStageBalance + item.Amount : lastTransactionStageBalance - item.Amount,
                            TransactionRole = budgetNotification.TransactionRole,
                            AccountId = budgetNotification.AccountId,
                            BudgetTransactionId = budgetTransactionId.Value,
                            BudgetAccountId = item.Id
                        };
                        var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetail);
                        decimal currentStageBalance = budgetTransactionDetail.StageBalance;

                        foreach (var budget in budgetTransactionAboveTransferedTime)
                        {
                            var updateBudgetTransactionDetail = budget.BudgetTransactionDetail.FirstOrDefault();
                            updateBudgetTransactionDetail.StageBalance = updateBudgetTransactionDetail.TransactionRole == TransactionRoleEnum.Receiver.EnumToString() ?
                                currentStageBalance + budget.Amount : currentStageBalance - budget.Amount;
                            Update(updateBudgetTransactionDetail);
                            currentStageBalance = updateBudgetTransactionDetail.StageBalance;
                        }
                        var budgetAccount = await GetBudgetAccountById(item.Id);
                        budgetAccount.Balance = currentStageBalance;
                        UpdateBudgetAccount(budgetAccount);
                    }
                }
                budgetNotification.NotificationStatus = NotificationStatusEnum.Accepted.EnumToString();
                UpdateBudgetNotification(budgetNotification);
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
        public async Task<bool> RejectBudgetTransaction(int BudgetNotificationId)
        {
            var budgetNotification = await dataContext.BudgetNotification.FirstOrDefaultAsync(x => x.BudgetNotificationId == BudgetNotificationId);
            budgetNotification.NotificationStatus = NotificationStatusEnum.Rejected.EnumToString();
            UpdateBudgetNotification(budgetNotification);
            return true;
        }
        public async Task<bool> IsCustomerAsBudgetAccount(int customerId)
        {
            return await dataContext.BudgetAccount.Where(x => x.CustomerId == customerId).AnyAsync();
        }
        public async Task<List<GetBudgetNotificationList>> GetBudgetNotificationList()
        {
            return await dataContext.BudgetNotification
                .Where(x => x.CustomerId == dataContext.CustomerId && x.NotificationStatus == NotificationStatusEnum.Pending.EnumToString())
                .Select(z => new GetBudgetNotificationList
                {
                    BudgetNotificationId = z.BudgetNotificationId,
                    TransactionType = z.TransactionType,
                    TransactionCatagory = z.TransactionCatagory,
                    Description = z.Description,
                    Amount = z.Amount,
                    TransactionDate = z.TransactionDate,
                    NotificationStatus = z.NotificationStatus.NotificationStatusStringToEnum(),
                    IsSelected = false
                }).ToListAsync();
        }
        public async Task<bool> ChangeBudgetAccountStatus(int budgetAccountId)
        {
            var budgetAccount = await dataContext.BudgetAccount.Where(x => x.BudgetAccountId == budgetAccountId).FirstOrDefaultAsync();
            if (budgetAccount != null)
            {
                budgetAccount.BudgetStatus = budgetAccount.BudgetStatus == BudgetAccountStatusEnum.Active.EnumToString() ? BudgetAccountStatusEnum.InActive.EnumToString() : BudgetAccountStatusEnum.Active.EnumToString();
                UpdateBudgetAccount(budgetAccount);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> SaveBudgetTransfer(BudgetAccount fromBudgetAccount, BudgetTransaction budgetTransaction,
            BudgetTransactionDetail fromBudgetTransactionDetail, List<BudgetTransactionDto> budgetAccountList)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                UpdateBudgetAccount(fromBudgetAccount);
                var fromBudgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                fromBudgetTransactionDetail.BudgetTransactionId = fromBudgetTransactionId.Value;
                InsertBudgetTransactionDetail(fromBudgetTransactionDetail);

                foreach (var toAccount in budgetAccountList)
                {
                    UpdateBudgetAccount(toAccount.BudgetAccount);
                    var toBudgetTransactionId = InsertBudgetTransaction(toAccount.BudgetTransaction);
                    toAccount.BudgetTransactionDetail.BudgetTransactionId = toBudgetTransactionId.Value;
                    InsertBudgetTransactionDetail(toAccount.BudgetTransactionDetail);
                }

                sqlTransaction.Commit();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return await Task.FromResult(false);
            }
        }
        #region Private
        private int? InsertBudgetAccount(BudgetAccount BudgetAccount)
        {
            BudgetAccount.CreatedBy = dataContext.UserEmail;
            BudgetAccount.CreatedDate = DateTime.Now;
            BudgetAccount.ModifiedBy = null;
            BudgetAccount.ModifiedDate = null;
            BudgetAccount.IsDeleted = false;
            var isInserted = Insert(BudgetAccount);
            return isInserted ? BudgetAccount.BudgetAccountId : null;
        }
        private void UpdateBudgetAccount(BudgetAccount BudgetAccount)
        {
            BudgetAccount.ModifiedBy = dataContext.UserEmail;
            BudgetAccount.ModifiedDate = DateTime.Now;
            Update(BudgetAccount);
        }
        private int? InsertBudgetTransaction(BudgetTransaction BudgetTransaction)
        {
            BudgetTransaction.CreatedBy = dataContext.UserEmail;
            BudgetTransaction.CreatedDate = DateTime.Now;
            BudgetTransaction.ModifiedBy = null;
            BudgetTransaction.ModifiedDate = null;
            BudgetTransaction.IsDeleted = false;
            var isInserted = Insert(BudgetTransaction);
            return isInserted ? BudgetTransaction.BudgetTransactionId : null;
        }
        private int? InsertBudgetTransactionDetail(BudgetTransactionDetail BudgetTransactionDetail)
        {
            var isInserted = Insert(BudgetTransactionDetail);
            return isInserted ? BudgetTransactionDetail.BudgetTransactionDetailId : null;
        }
        private void UpdateBudgetNotification(BudgetNotification model)
        {
            model.ModifiedBy = dataContext.UserEmail;
            model.ModifiedDate = DateTime.Now;
            Update(model);
        }
        #endregion Private
    }
}
