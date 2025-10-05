using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class WalletService(IWalletRepository walletRepository, IBudgetRepository budgetRepository,
        IBankAccountRepository bankAccountRepository, ICommonService commonService) : IWalletService
    {
        private readonly IWalletRepository _walletRepository = walletRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly ICommonService _commonService = commonService;
        public async Task<ResponseDto<bool>> CreateWalletAccount(string walletName, decimal amount)
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
                WalletStatus = WalletStatusEnum.Active.EnumToString()
            };
            WalletTransaction walletTransaction = new()
            {
                Amount = amount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCategoryEnum.Deposit.EnumToString(),
                TransactionTag = TransactionTagEnum.Deposit.EnumToString()
            };
            WalletTransactionDetail walletTransactionDetails = new()
            {
                TransactionType = TransactionTypeEnum.Credit.EnumToString(),
                StageBalance = amount,
                TransactionRole = TransactionRoleEnum.Receiver.EnumToString()
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
                    WalletStatus = wallet.WalletStatus.WalletStatusStringToEnum(),
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
        public async Task<ResponseDto<bool>> DepositWalletAmount(int walletId, decimal amount, string description, DateTime transactionDate, List<DepositWalletAmountRequestBudgetAccount> budgetAccounts,
            bool isFromAccount, int? accountId)
        {
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
            {
                Id = budgetAccounts.Id,
                Amount = budgetAccounts.Amount
            })];
            var (budgetDetails, budgetValidationResult) = await _commonService.GetBudgetAccountDetailsToDebitOrCredit(false, accountId, amount, description, transactionDate, validateBudgetAccount, TransactionTagEnum.Transfer, TransactionCategoryEnum.Transfer);

            if (budgetValidationResult != null && budgetValidationResult.Count != 0)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = budgetValidationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }

            var (walletDetails, walletValidationResult) = await _commonService.GetWalletDetailsToDebitOrCredit(false, walletId, amount, description, transactionDate, TransactionCategoryEnum.Deposit, TransactionTagEnum.Deposit);

            if (walletValidationResult != null && walletValidationResult.Count != 0)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = walletValidationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }

            (AccountTransactionsDto accountDetails, List<ValidationResults> accountValidationResult) = (null, null);
            if (isFromAccount)
            {
                (accountDetails, accountValidationResult) = await _commonService.GetAccountDetailsToDebitOrCredit(true, accountId, amount, description, transactionDate, TransactionTagEnum.Transfer, TransactionCategoryEnum.Transfer);
                if (accountValidationResult != null && accountValidationResult.Count != 0)
                {
                    return new ResponseDto<bool>
                    {
                        Response = false,
                        Status = false,
                        ValidationErrors = accountValidationResult,
                        Errors = [],
                        StatuCode = 400
                    };
                }
            }

            var isDeposited = _walletRepository.SaveTransactions(walletDetails, budgetDetails, accountDetails);
            return new ResponseDto<bool>
            {
                Response = isDeposited,
                Status = isDeposited,
                ValidationErrors = [],
                Errors = isDeposited ? [] : [new Errors() { PropertyName = "WalletDeposit", ErrorMessage = "Failed to Save Wallet Deposit Transaction." }]
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
                    new DropDownListDto{ Name = "BudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() },
                    new DropDownListDto{ Name = "FromAccount", DropDown = await _bankAccountRepository.GetFromAccountListByCustomerId() }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> GetWalletTransferInitialLoad()
        {
            return new ResponseDto<bool>
            {
                Response = true,
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{ Name = "FromWalletAccount", DropDown = await _walletRepository.GetFromWalletAccountListByCustomerId() },
                    new DropDownListDto{ Name = "TransactionTag", DropDown = await _bankAccountRepository.GetAllTransactionTag() },
                    new DropDownListDto{ Name = "BudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() },
                    new DropDownListDto{ Name = "Account", DropDown = await _bankAccountRepository.GetAllAccountList() }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }

        public async Task<ResponseDto<bool>> WalletTransfer(int walletId, int? accountId, decimal amount, string description, TransactionTagEnum transactionTag,
            DateTime transactionDate, List<WalletTransferRequestBudgetAccount> budgetAccounts)
        {
            (List<BudgetTransactionDto> budgetDetails, List<ValidationResults> budgetValidationResult) = ([], null);
            (AccountTransactionsDto accountDetails, List<ValidationResults> accountValidationResult) = (null, null);
            if (accountId == null)
            {
                List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
                {
                    Id = budgetAccounts.Id,
                    Amount = budgetAccounts.Amount
                })];
                (budgetDetails, budgetValidationResult) = await _commonService.GetBudgetAccountDetailsToDebitOrCredit(true, null, amount, description, transactionDate, validateBudgetAccount, transactionTag, TransactionCategoryEnum.Transfer);
                if (budgetValidationResult != null && budgetValidationResult.Count != 0)
                {
                    return new ResponseDto<bool>
                    {
                        Response = false,
                        Status = false,
                        ValidationErrors = budgetValidationResult,
                        Errors = [],
                        StatuCode = 400
                    };
                }
            }
            if(accountId != null)
            {
                (accountDetails, accountValidationResult) = await _commonService.GetAccountDetailsToDebitOrCredit(false, accountId, amount, description, transactionDate, transactionTag, TransactionCategoryEnum.Transfer);
                if (accountValidationResult != null && accountValidationResult.Count != 0)
                {
                    return new ResponseDto<bool>
                    {
                        Response = false,
                        Status = false,
                        ValidationErrors = accountValidationResult,
                        Errors = [],
                        StatuCode = 400
                    };
                }
            }
            var (walletDetails, walletValidationResult) = await _commonService.GetWalletDetailsToDebitOrCredit(true, walletId, amount, description, transactionDate, TransactionCategoryEnum.Transfer, transactionTag);

            if (walletValidationResult != null && walletValidationResult.Count != 0)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = walletValidationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }
            var isTransfered = _walletRepository.SaveTransactions(walletDetails, budgetDetails, accountDetails);
            return new ResponseDto<bool>
            {
                Response = isTransfered,
                Status = isTransfered,
                ValidationErrors = [],
                Errors = isTransfered ? [] : [new Errors() { PropertyName = "WalletTransfer", ErrorMessage = "Failed to Save Wallet Transfer Transaction." }]
            };
        }
        public async Task<ResponseDto<decimal>> GetWalletBalance(int accountId)
        {
            return new ResponseDto<decimal>
            {
                Response = await _walletRepository.GetWalletBalance(accountId),
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
        #endregion Private
    }
}
