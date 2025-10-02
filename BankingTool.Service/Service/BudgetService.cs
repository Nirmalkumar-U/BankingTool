using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BudgetService(IBudgetRepository budgetRepository) : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        public async Task<ResponseDto<bool>> CreateBudgetAccount(string budgetName, int amount)
        {
            ResponseDto<bool> response = new()
            {
                Errors = [],
                ValidationErrors = [],
                Status = false,
            };
            var budgetAccount = new BudgetAccount()
            {
                BudgetName = budgetName,
                BudgetNumber = await GetNewBudgetNumber(),
                Balance = amount,
                BudgetStatus = BudgetAccountStatusEnum.Active.EnumToString(),
            };
            BudgetTransaction budgetTransaction = new()
            {
                Amount = amount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCategoryEnum.Deposit.EnumToString(),
                TransactionTag = TransactionTagEnum.Deposit.EnumToString()
            };
            BudgetTransactionDetail budgetTransactionDetails = new()
            {
                TransactionType = TransactionTypeEnum.Credit.EnumToString(),
                StageBalance = amount,
                TransactionRole = TransactionRoleEnum.Receiver.EnumToString(),
                AccountId = await _budgetRepository.GetPrimaryAccountIdOfCustomer()
            };

            var createdBudgerAccount = await _budgetRepository.CreateBudgetAccount(budgetAccount, budgetTransaction, budgetTransactionDetails);

            response.Status = createdBudgerAccount;
            response.Response = createdBudgerAccount;
            response.StatuCode = createdBudgerAccount ? 200 : 400;
            if (!createdBudgerAccount)
                response.Errors.Add(new Errors { PropertyName = "Create Budget Account", ErrorMessage = "Failed to create budget account" });
            response.Message = createdBudgerAccount ? "Budget Account Created Successfully" : "Failed to create budget account";
            return response;
        }
        public async Task<ResponseDto<bool>> AcceptBudgetTransaction(List<AcceptBudgetTransactionRequestBudgetAcccount> BudgetAccount, int BudgetNotificationId)
        {
            if (BudgetAccount == null || BudgetAccount.Count == 0)
            {
                return new ResponseDto<bool>
                {
                    DropDownList = [],
                    Errors = [],
                    Message = "Failed to accept.",
                    Response = false,
                    StatuCode = 400,
                    Status = false,
                    ValidationErrors = [new ValidationResults { PropertyPath = "Request.BudgetAccount", PropertyName = "BudgetAccount", ErrorMessage = "BudgetAccount is required." }]
                };
            }
            var budgetNotification = await _budgetRepository.GetBudgetNotification(BudgetNotificationId);
            int sumOfBudgetAmount = BudgetAccount.Sum(x => x.Amount);
            if (budgetNotification.Amount < sumOfBudgetAmount)
            {
                return new ResponseDto<bool>
                {
                    DropDownList = [],
                    Errors = [],
                    Message = "Failed to accept.",
                    Response = false,
                    StatuCode = 400,
                    Status = false,
                    ValidationErrors = [new ValidationResults { PropertyPath = "AcceptBudgetTransaction", PropertyName = "AcceptBudgetTransaction", ErrorMessage = "Sum of budget amount should be lesser than equal to budget notification amount." }]
                };
            }
            var isAccepted = await _budgetRepository.AcceptBudgetTransaction(BudgetAccount, BudgetNotificationId);
            return new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = isAccepted ? [] : [new Errors { PropertyName = "Accept Budget Transaction", ErrorMessage = "Failed to AcceptBudgetTransaction." }],
                Message = isAccepted ? "Accepted successfully." : "Failed to accept.",
                Response = isAccepted,
                StatuCode = isAccepted ? 200 : 400,
                Status = isAccepted,
                ValidationErrors = []
            };
        }
        public async Task<ResponseDto<bool>> RejectBudgetTransaction(int BudgetNotificationId)
        {
            var isRejected = await _budgetRepository.RejectBudgetTransaction(BudgetNotificationId);
            return new ResponseDto<bool>
            {
                DropDownList = [],
                Errors = isRejected ? [] : [new Errors { PropertyName = "Reject Budget Transaction", ErrorMessage = "Failed to RejectBudgetTransaction." }],
                Message = isRejected ? "Rejected successfully." : "Failed to Reject.",
                Response = isRejected,
                StatuCode = isRejected ? 200 : 400,
                Status = isRejected,
                ValidationErrors = []
            };
        }
        public async Task<ResponseDto<GetBudgetListResponse>> GetBudgetList()
        {
            return new ResponseDto<GetBudgetListResponse>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = new GetBudgetListResponse
                {
                    BudgetList = await _budgetRepository.GetBudgetList()
                },
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<bool>> ChangeBudgetAccountStatus(int budgetAccountId)
        {
            return new ResponseDto<bool>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = await _budgetRepository.ChangeBudgetAccountStatus(budgetAccountId),
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<GetBudgetTransactionListResponse>> GetBudgetTransactionList(int budgetAccountId)
        {
            var budgetAccount = await _budgetRepository.GetBudgetAccountById(budgetAccountId);
            return new ResponseDto<GetBudgetTransactionListResponse>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = new GetBudgetTransactionListResponse
                {
                    BudgetName = budgetAccount.BudgetName,
                    BudgetNumber = budgetAccount.BudgetNumber,
                    BudgetStatus = budgetAccount.BudgetStatus.BudgetAccountStatusStringToEnum(),
                    Balance = budgetAccount.Balance,
                    BudgetTransactionList = await _budgetRepository.GetBudgetTransactionList(budgetAccountId)
                },
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<GetBudgetNotificationListResponse>> GetBudgetNotificationList()
        {
            var budgetList = await _budgetRepository.GetBudgetList();
            var budgetDropdownList = budgetList.Where(z => z.BudgetStatus == BudgetAccountStatusEnum.Active).Select(x => new DropDownDto
            {
                Key = x.BudgetAccountId,
                Value = x.BudgetName,
                IsSelected = false
            }).ToList();
            return new ResponseDto<GetBudgetNotificationListResponse>
            {
                DropDownList = [new DropDownListDto { Name = "BudgetAccount", DropDown = budgetDropdownList }],
                Errors = [],
                StatuCode = 200,
                ValidationErrors = [],
                Response = new GetBudgetNotificationListResponse
                {
                    budgetNotificationList = await _budgetRepository.GetBudgetNotificationList()
                },
                Status = true
            };
        }
        public async Task<ResponseDto<bool>> GetBudgetDropdownList()
        {
            return new ResponseDto<bool>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = true,
                Status = true,
                StatuCode = 200,
                DropDownList = [new DropDownListDto { Name = "FromBudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() }]
            };
        }
        public async Task<ResponseDto<int>> GetToBudgetDropdownListAndBalanceOfBudgetAccount(int bugetAccountId)
        {
            return new ResponseDto<int>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = await _budgetRepository.GetBudgetAccountBalance(bugetAccountId),
                Status = true,
                StatuCode = 200,
                DropDownList = [new DropDownListDto { Name = "ToBudgetAccountId", DropDown = await _budgetRepository.GetBudgetAccountDropdownExceptBudgetAccountId(bugetAccountId) }]
            };
        }
        public async Task<ResponseDto<bool>> BudgetTransfer(int fromAccountId, List<SaveBudgetTransactionRequestToBudgetAccount> toAccounts, int amount, string description)
        {
            var fromBudgetAccount = await _budgetRepository.GetBudgetAccountById(fromAccountId);
            var sumOfToAccountAmount = toAccounts.Sum(x => x.Amount);
            if (sumOfToAccountAmount != amount)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [new ValidationResults { PropertyPath = "Request.FromBudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "From amount and to amount should be equal." }],
                    Errors = [],
                    StatuCode = 400
                };
            }
            if(fromBudgetAccount.Balance < amount)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [new ValidationResults { PropertyPath = "Request.FromBudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "From amount should be lesser than Budget Account Balance." }],
                    Errors = [],
                    StatuCode = 400
                };
            }
            var validateBalance = ValidateBalance(fromBudgetAccount.Balance, amount);
            if (validateBalance != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validateBalance,
                    Errors = [],
                    StatuCode = 400
                };
            }
            var lastTransaction = await _budgetRepository.GetLastBudgetTransaction(fromAccountId);
            var transactionTime = DateTime.Now.Date;
            if (lastTransaction.TransactionTime.Date == transactionTime)
            {
                transactionTime = lastTransaction.TransactionTime.AddMinutes(5);
            }
            fromBudgetAccount.Balance -= amount;
            BudgetTransaction budgetTransaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionTime,
                TransactionCategory = TransactionCategoryEnum.Transfer.EnumToString(),
                TransactionTag = TransactionTagEnum.Transfer.EnumToString()
            };
            BudgetTransactionDetail senderBudgetTransactionDetails = new()
            {
                TransactionType = TransactionTypeEnum.Debit.EnumToString(),
                StageBalance = fromBudgetAccount.Balance,
                TransactionRole = TransactionRoleEnum.Sender.EnumToString(),
                AccountId = 1,
                BudgetAccountId = fromAccountId
            };

            List<BudgetTransactionDto> budgetAccountList = [];
            foreach (var toAccount in toAccounts)
            {
                var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(toAccount.Id);
                if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionTime.Date)
                {
                    return new ResponseDto<bool>
                    {
                        Response = false,
                        Status = false,
                        ValidationErrors = [],
                        Errors = [new Errors { PropertyName = "BudgetTransferAmount", ErrorMessage = "Budget Transaction Date should be greater then last transaction date." }]
                    };
                }
                var budgetTransactionDate = transactionTime;
                if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                {
                    budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                    budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                }

                var toBudgerAccount = await _budgetRepository.GetBudgetAccountById(toAccount.Id);
                toBudgerAccount.Balance += toAccount.Amount;

                BudgetTransaction toBudgetTransaction = new()
                {
                    Amount = toAccount.Amount,
                    Description = description,
                    TransactionTime = budgetTransactionDate,
                    TransactionCategory = TransactionCategoryEnum.Transfer.EnumToString(),
                    TransactionTag = TransactionTagEnum.Transfer.EnumToString()
                };
                BudgetTransactionDetail receiverBudgetTransactionDetails = new()
                {
                    TransactionType = TransactionTypeEnum.Credit.EnumToString(),
                    StageBalance = toBudgerAccount.Balance,
                    TransactionRole = TransactionRoleEnum.Receiver.EnumToString(),
                    AccountId = 1,
                    BudgetAccountId = toAccount.Id
                };
                BudgetTransactionDto budgetTransactions = new BudgetTransactionDto()
                {
                    BudgetAccount = toBudgerAccount,
                    BudgetTransaction = toBudgetTransaction,
                    BudgetTransactionDetail = receiverBudgetTransactionDetails
                };
                budgetAccountList.Add(budgetTransactions);
            }
            var isTransfered = await _budgetRepository.SaveBudgetTransfer(fromBudgetAccount, budgetTransaction, senderBudgetTransactionDetails, budgetAccountList);
            return new ResponseDto<bool>
            {
                Response = isTransfered,
                Status = isTransfered,
                ValidationErrors = [],
                Errors = isTransfered ? [] : [new Errors { PropertyName = "Budget Amount transfer.", ErrorMessage = "Budget Amount transfer failed." }],
                StatuCode = isTransfered ? 200 : 400,
                DropDownList = []
            };
        }
        #region Private
        private async Task<string> GetNewBudgetNumber()
        {
            var lastBudgetAccount = await _budgetRepository.GetLastBudgetAccount();
            return ((lastBudgetAccount != null && long.TryParse(lastBudgetAccount.BudgetNumber, out long lastNumber)) ? lastNumber + 1 : 60000000).ToString();
        }
        private static List<ValidationResults> ValidateBalance(int balance, int amount)
        {
            if (balance < amount)
            {
                return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Amount should be lesser equal to Account Balance." }];
            }
            return null;
        }
        #endregion Private
    }
}
