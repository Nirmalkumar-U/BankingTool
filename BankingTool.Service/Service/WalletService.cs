using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class WalletService(IWalletRespository walletRepository, IBudgetRepository budgetRepository, IBankAccountRepository bankAccountRepository) : IWalletService
    {
        private readonly IWalletRespository _walletRepository = walletRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IBankAccountRepository _bankAccountRepository= bankAccountRepository;
        public async Task<ResponseDto<bool>> CreateWalletAccount(string walletName, int amount)
        {
            ResponseDto<bool> response = new()
            {
                Errors = [],
                ValidationErrors = [],
                Status = false,
            };
            var walletAccount = new WalletAccount()
            {
                WalletName = walletName,
                WalletNumber = await GetNewBudgetNumber(),
                Balance = amount,
                WalletStatus = "Active"
            };
            WalletTransaction walletTransaction = new()
            {
                Amount = amount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCatagory.Deposit,
                TransactionTag = TransactionTag.Deposit
            };
            WalletTransactionDetail walletTransactionDetails = new()
            {
                TransactionType = TransactionType.Credit,
                StageBalance = amount,
                TransactionRole = TransactionRole.Receiver
            };

            var createdBudgerAccount = await _walletRepository.CreateWalletAccount(walletAccount, walletTransaction, walletTransactionDetails);

            response.Status = createdBudgerAccount;
            response.Response = createdBudgerAccount;
            response.StatuCode = createdBudgerAccount ? 200 : 400;
            if (!createdBudgerAccount)
                response.Errors.Add(new Errors { PropertyName = "Create Wallet Account", ErrorMessage = "Failed to create Wallet account" });
            response.Message = createdBudgerAccount ? "Wallet Account Created Successfully" : "Failed to create Wallet account";
            return response;
        }
        public async Task<ResponseDto<GetWalletListResponse>> GetWalletList()
        {
            return new ResponseDto<GetWalletListResponse>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = new GetWalletListResponse
                {
                    WalletList = await _walletRepository.GetWalletList()
                },
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<GetWalletTransactionListResponse>> GetWalletTransactionList(int walletId)
        {
            var wallet = await _walletRepository.GetWalletById(walletId);
            return new ResponseDto<GetWalletTransactionListResponse>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = new GetWalletTransactionListResponse
                {
                    WalletName = wallet.WalletName,
                    WalletNumber = wallet.WalletNumber,
                    WalletStatus = wallet.WalletStatus,
                    Balance = wallet.Balance,
                    WalletTransactionList = await _walletRepository.GetWalletTransactionList(walletId)
                },
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<bool>> ChangeWalletStatus(int walletId)
        {
            return new ResponseDto<bool>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = await _walletRepository.ChangeWalletStatus(walletId),
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<bool>> DepositWalletAmount(int walletId, int amount, string description, DateTime transactionDate, List<DepositWalletAmountRequestBudgetAccount> budgetAccounts)
        {
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
            {
                Id = budgetAccounts.Id,
                Amount = budgetAccounts.Amount
            })];
            var validationResult = ValidateBudgetAccount(validateBudgetAccount, amount);
            if (validationResult != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }

            var lastTransaction = await _walletRepository.GetLastWalletTransaction(walletId);
            if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "DepositAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                };
            }
            if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
            {
                transactionDate = lastTransaction.TransactionTime;
                transactionDate = transactionDate.AddMinutes(5);
            }
            WalletAccount walletAccount = await _walletRepository.GetWalletAccount(walletId);
            walletAccount.Balance = walletAccount.Balance + amount;

            WalletTransaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionDate,
                TransactionCategory = TransactionCatagory.Deposit,
                TransactionTag = TransactionTag.Deposit
            };
            WalletTransactionDetail transactionDetails = new()
            {
                WalletAccountId = walletAccount.WalletAccountId,
                TransactionType = TransactionType.Credit,
                StageBalance = walletAccount.Balance,
                TransactionRole = TransactionRole.Receiver,
            };

            List<BudgetTransactionDto> budgetAccountList = [];
            bool isBudgetAccountAttached = false;
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                isBudgetAccountAttached = true;
                foreach (var bAccount in budgetAccounts)
                {
                    var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                    {
                        return new ResponseDto<bool>
                        {
                            Response = false,
                            Status = false,
                            ValidationErrors = [],
                            Errors = [new Errors { PropertyName = "DepositAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                        };
                    }
                    var budgetTransactionDate = transactionDate;
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                    {
                        budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                        budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                    }
                    BudgetAccount budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                    budgetAccount.Balance = budgetAccount.Balance + bAccount.Amount;
                    BudgetTransaction budgetTransaction = new BudgetTransaction()
                    {
                        Amount = bAccount.Amount,
                        Description = description,
                        TransactionTime = budgetTransactionDate,
                        TransactionCategory = TransactionCatagory.Deposit,
                        TransactionTag = TransactionTag.Deposit
                    };
                    BudgetTransactionDetail budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = TransactionType.Credit,
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = TransactionRole.Receiver,
                        AccountId = 1,//TODO: Need to replace with actual value
                        BudgetAccountId = budgetAccount.BudgetAccountId
                    };
                    BudgetTransactionDto budgetTransactions = new BudgetTransactionDto()
                    {
                        BudgetAccount = budgetAccount,
                        BudgetTransaction = budgetTransaction,
                        BudgetTransactionDetail = budgetTransactionDetails
                    };
                    budgetAccountList.Add(budgetTransactions);
                }
            }

            _walletRepository.WalletDeposit(walletAccount, transaction, transactionDetails, isBudgetAccountAttached, budgetAccountList);
            return new ResponseDto<bool>
            {
                Response = true,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> GetWalletDepositInitialLoad()
        {
            return new ResponseDto<bool>
            {
                Response = true,
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{ Name = "FromWalletAccount", DropDown = await _walletRepository.GetFromWalletAccountListByCustomerId() },
                    new DropDownListDto{ Name = "TransactionTag", DropDown = await _bankAccountRepository.GetAllTransactionTag() },
                    new DropDownListDto{ Name = "BudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        #region Private
        private async Task<string> GetNewBudgetNumber()
        {
            var lastWalletAccount = await _walletRepository.GetLastWalletAccount();
            return ((lastWalletAccount != null && long.TryParse(lastWalletAccount.WalletNumber, out long lastNumber)) ? lastNumber + 1 : 50000000).ToString();
        }
        private static List<ValidationResults> ValidateBudgetAccount(List<BudgetAccountDto> budgetAccounts, int amount)
        {
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                bool isInValid = budgetAccounts.Any(x => x.Id == null);
                bool isAmountInValid = budgetAccounts.Any(x => x?.Amount == null || x.Amount == 0);
                int sumOfBudgetAccountAmount = budgetAccounts.Sum(x => x.Amount);
                if (sumOfBudgetAccountAmount > amount)
                {
                    return [new ValidationResults { PropertyPath = "BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Sum of Budget Account Amount should be lesser equal to Transfer Amount." }];
                }
                if (isInValid)
                {
                    return [new ValidationResults { PropertyPath = "BudgetAccount.Id", PropertyName = "BudgetAccountId", ErrorMessage = "BudgetAccountId is required." }];
                }
                if (isAmountInValid)
                {
                    return [new ValidationResults { PropertyPath = "BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Amount is required." }];
                }
            }
            return null;
        }
        #endregion Private
    }
}
