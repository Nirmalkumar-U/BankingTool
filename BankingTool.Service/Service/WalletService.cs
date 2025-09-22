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
    public class WalletService(IWalletRepository walletRepository, IBudgetRepository budgetRepository,
        IBankAccountRepository bankAccountRepository, ICommonService commonService) : IWalletService
    {
        private readonly IWalletRepository _walletRepository = walletRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly ICommonService _commonService = commonService;
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
        public async Task<ResponseDto<bool>> DepositWalletAmount(int walletId, int amount, string description, DateTime transactionDate, List<DepositWalletAmountRequestBudgetAccount> budgetAccounts,
            bool isFromAccount, int? accountId)
        {
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
            {
                Id = budgetAccounts.Id,
                Amount = budgetAccounts.Amount
            })];
            var (budgetDetails, budgetValidationResult) = await _commonService.GetBudgetAccountDetailsToDebitOrCredit(false, accountId, amount, description, transactionDate, validateBudgetAccount, TransactionTag.Transfer, TransactionCatagory.Transfer);

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

            var (walletDetails, walletValidationResult) = await _commonService.GetWalletDetailsToDebitOrCredit(false, walletId, amount, description, transactionDate, TransactionCatagory.Deposit, TransactionTag.Deposit);

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
                (accountDetails, accountValidationResult) = await _commonService.GetAccountDetailsToDebitOrCredit(true, accountId, amount, description, transactionDate, TransactionCatagory.Transfer, TransactionTag.Transfer);
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

            var isDeposited = _walletRepository.WalletDeposit(walletDetails, budgetDetails,accountDetails);
            return new ResponseDto<bool>
            {
                Response = isDeposited,
                Status = isDeposited,
                ValidationErrors = [],
                Errors = isDeposited ? []: [new Errors() { PropertyName = "WalletDeposit", ErrorMessage = "Failed to Save Wallet Deposit Transaction." }]
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
        #region Private
        private async Task<string> GetNewBudgetNumber()
        {
            var lastWalletAccount = await _walletRepository.GetLastWalletAccount();
            return ((lastWalletAccount != null && long.TryParse(lastWalletAccount.WalletNumber, out long lastNumber)) ? lastNumber + 1 : 50000000).ToString();
        }
        #endregion Private
    }
}
