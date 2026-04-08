using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Dto.RequestDto.BulkTransaction;
using BankingTool.Model.Dto.RequestDto.Calender;
using BankingTool.Model.Dto.RequestDto.Note;
using BankingTool.Model.Dto.RequestDto.User;
using BankingTool.Model.Dto.RequestDto.Wallet;

namespace BankingTool.Api.Validators
{
    public class ValidationRules
    {
        #region UserController
        public static List<ValidationRule> LoginRequestRules = new()
        {
            RuleBuilder<LoginRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User).Required(),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User.Email)
                .Required()
                .WithStringRules(r => {
                    r.EmailFormat = true;
                    r.NotEmpty = true;
                }),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User.Password)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 6;
                    r.MaxLength = 15;
                }),
        };
        public static List<ValidationRule> CreateTokenRequestRules = new()
        {
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.Role).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.Email)
                .Required()
                .WithStringRules(r => {
                    r.EmailFormat = true;
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.FirstName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.LastName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.Role.Id).Required(),
        };
        public static List<ValidationRule> SaveUserRequestRules = new()
        {
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.User).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.Role).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.State).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.City).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.User.Email)
                .Required()
                .WithStringRules(r => {
                    r.EmailFormat = true;
                    r.NotEmpty = true;
                }),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.User.FirstName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.User.LastName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.User.Password)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 6;
                }),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.Role.Id).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.State.Id).Required(),
            RuleBuilder<SaveUserRequestObject>.For(x => x.Request.City.Id).Required(),
        };
        public static List<ValidationRule> GetUserInitialLoadRequestRules = new()
        {
            RuleBuilder<GetUserInitialLoadRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetUserInitialLoadRequestObject>.For(x => x.Request.User).Required()
        };
        public static List<ValidationRule> GetCityListRequestRules = new()
        {
            RuleBuilder<GetCityListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetCityListRequestObject>.For(x => x.Request.State).Required(),
            RuleBuilder<GetCityListRequestObject>.For(x => x.Request.State.Id).Required()
        };
        #endregion UserController
        #region BankAccountController
        public static List<ValidationRule> CreateAccountRequestRules = new()
        {
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Bank).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Bank.Id).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Customer.Id).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Account.AccountTypeId).Required(),
            RuleBuilder<CreateAccountRequestObject>.For(x => x.Request.Account.DoYouWantToChangeThisAccountToPrimaryAccount).Required(),
        };
        public static List<ValidationRule> GetBankDetailWithoutCustomerAndAccountTypeRequestRules = new()
        {
            RuleBuilder<GetBankDetailWithoutCustomerAndAccountTypeRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetBankDetailWithoutCustomerAndAccountTypeRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<GetBankDetailWithoutCustomerAndAccountTypeRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<GetBankDetailWithoutCustomerAndAccountTypeRequestObject>.For(x => x.Request.Customer.Id).Required(),
            RuleBuilder<GetBankDetailWithoutCustomerAndAccountTypeRequestObject>.For(x => x.Request.Account.AccountTypeId).Required()
        };
        public static List<ValidationRule> IsCustomerHasCreditCardInThatBankRequestRules = new()
        {
            RuleBuilder<IsCustomerHasCreditCardInThatBankRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<IsCustomerHasCreditCardInThatBankRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<IsCustomerHasCreditCardInThatBankRequestObject>.For(x => x.Request.Bank).Required(),
            RuleBuilder<IsCustomerHasCreditCardInThatBankRequestObject>.For(x => x.Request.Customer.Id).Required(),
            RuleBuilder<IsCustomerHasCreditCardInThatBankRequestObject>.For(x => x.Request.Bank.Id).Required()
        };
        public static List<ValidationRule> TransactionsListForCustomerRequestRules = new()
        {
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Bank).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Customer.Id).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Account.AccountTypeId).Required(),
            RuleBuilder<TransactionsListForCustomerRequestObject>.For(x => x.Request.Bank.Id).Required()
        };
        public static List<ValidationRule> BankDropDownListRequestRules = new()
        {
            RuleBuilder<BankDropDownListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<BankDropDownListRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<BankDropDownListRequestObject>.For(x => x.Request.Customer.Id).Required()
        };
        public static List<ValidationRule> GetAccountTypeDropDownListRequestRules = new()
        {
            RuleBuilder<GetAccountTypeDropDownListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetAccountTypeDropDownListRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<GetAccountTypeDropDownListRequestObject>.For(x => x.Request.Bank).Required(),
            RuleBuilder<GetAccountTypeDropDownListRequestObject>.For(x => x.Request.Customer.Id).Required(),
            RuleBuilder<GetAccountTypeDropDownListRequestObject>.For(x => x.Request.Bank.Id).Required()
        };
        public static List<ValidationRule> GetTransferAmountInitialLoadRequestRules = new()
        {
            RuleBuilder<GetTransferAmountInitialLoadRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetTransferAmountInitialLoadRequestObject>.For(x => x.Request.Customer).Required(),
            RuleBuilder<GetTransferAmountInitialLoadRequestObject>.For(x => x.Request.Customer.Id).Required()
        };
        public static List<ValidationRule> GetAccountBalanceRequestRules = new()
        {
            RuleBuilder<GetAccountBalanceRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetAccountBalanceRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<GetAccountBalanceRequestObject>.For(x => x.Request.Account.Id).Required()
        };
        public static List<ValidationRule> GetToAccountListExcludedByAccountIdRequestRules = new()
        {
            RuleBuilder<GetToAccountListExcludedByAccountIdRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetToAccountListExcludedByAccountIdRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<GetToAccountListExcludedByAccountIdRequestObject>.For(x => x.Request.Account.Id).Required()
        };
        public static List<ValidationRule> TransferAmountRequestRules = new()
        {
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Transaction).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Account.FromAccountId).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Account.ToAccountId).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Transaction.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Transaction.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Transaction.TransactionDate).Required(),
            RuleBuilder<TransferAmountRequestObject>.For(x => x.Request.Transaction.TransactionTag).Required()
        };
        public static List<ValidationRule> DepositAmountRequestObjectRules = new()
        {
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Transaction).Required(),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Account.Id).Required(),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Transaction.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Transaction.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<DepositAmountRequestObject>.For(x => x.Request.Transaction.TransactionDate).Required()
        };
        public static List<ValidationRule> CashWithdrawRequestObjectRules = new()
        {
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Transaction).Required(),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Account.Id).Required(),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Transaction.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Transaction.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<CashWithdrawRequestObject>.For(x => x.Request.Transaction.TransactionDate).Required()
        };
        public static List<ValidationRule> TransactionListRequestObjectRules = new()
        {
            RuleBuilder<TransactionListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<TransactionListRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<TransactionListRequestObject>.For(x => x.Request.Transaction).Required(),
            RuleBuilder<TransactionListRequestObject>.For(x => x.Request.Account.Id).Required()
        };
        public static List<ValidationRule> CreateBudgetAccountRequestObjectRules = new()
        {
            RuleBuilder<CreateBudgetAccountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CreateBudgetAccountRequestObject>.For(x => x.Request.BudgetAccount).Required(),
            RuleBuilder<CreateBudgetAccountRequestObject>.For(x => x.Request.BudgetAccount.BudgetName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<CreateBudgetAccountRequestObject>.For(x => x.Request.BudgetAccount.Amount).Required(),
        };
        public static List<ValidationRule> CreateGetBudgetTransactionListRequestObjectRules = new()
        {
            RuleBuilder<GetBudgetTransactionListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetBudgetTransactionListRequestObject>.For(x => x.Request.BudgetAccount).Required(),
            RuleBuilder<GetBudgetTransactionListRequestObject>.For(x => x.Request.BudgetAccount.Id).Required(),
        };
        public static List<ValidationRule> CreateAcceptBudgetTransactionRequestRules = new()
        {
            RuleBuilder<AcceptBudgetTransactionRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<AcceptBudgetTransactionRequestObject>.For(x => x.Request.BudgetAccount).Required(),
            RuleBuilder<AcceptBudgetTransactionRequestObject>.For(x => x.Request.BudgetNotification).Required(),
            RuleBuilder<AcceptBudgetTransactionRequestObject>.For(x => x.Request.BudgetNotification.Id).Required()
        };
        public static List<ValidationRule> CreateRejectBudgetTransactionRequestRules = new()
        {
            RuleBuilder<RejectBudgetTransactionRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<RejectBudgetTransactionRequestObject>.For(x => x.Request.BudgetNotification).Required(),
            RuleBuilder<RejectBudgetTransactionRequestObject>.For(x => x.Request.BudgetNotification.Id).Required()
        };
        public static List<ValidationRule> CreateChangeBudgetAccountStatusRequestRules = new()
        {
            RuleBuilder<ChangeBudgetAccountStatusRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<ChangeBudgetAccountStatusRequestObject>.For(x => x.Request.BudgetAccount).Required(),
            RuleBuilder<ChangeBudgetAccountStatusRequestObject>.For(x => x.Request.BudgetAccount.Id).Required()
        };
        #endregion BankAccountController
        #region Note
        public static List<ValidationRule> SaveNoteRequestRules = new()
        {
            RuleBuilder<SaveNoteRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<SaveNoteRequestObject>.For(x => x.Request.Note).Required(),
            RuleBuilder<SaveNoteRequestObject>.For(x => x.Request.Note.Title)
                .Required()
                .WithStringRules(r => {
                    r.MaxLength = 100;
                }),
            RuleBuilder<SaveNoteRequestObject>.For(x => x.Request.Note.Content)
                .Required()
                .WithStringRules(r => {
                    r.MaxLength = 5000;
                }),
        };
        public static List<ValidationRule> DeleteNoteRequestRules = new()
        {
            RuleBuilder<DeleteNoteRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<DeleteNoteRequestObject>.For(x => x.Request.Note).Required(),
            RuleBuilder<DeleteNoteRequestObject>.For(x => x.Request.Note.Id).Required()
        };
        #endregion Note
        #region Budget
        public static List<ValidationRule> SaveBudgetTransactionRequestRules = new()
        {
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.FromBudgetAccount).Required(),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.ToBudgetAccount).Required(),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.FromBudgetAccount.Id).Required(),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.ToBudgetAccount).Required()
                .WithCollectionRules(r => {
                    r.CollectionMinCount = 1;
                }),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.FromBudgetAccount.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<SaveBudgetTransactionRequestObject>.For(x => x.Request.FromBudgetAccount.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            //RuleBuilder<SaveBudgetTransactionRequestObject>.ForCollectionItem(x=>x.Request.ToBudgetAccount, x => x.Id).Required(),
            //RuleBuilder<SaveBudgetTransactionRequestObject>.ForCollectionItem(x=>x.Request.ToBudgetAccount, x => x.Amount).Required(),
            //RuleBuilder<SaveBudgetTransactionRequestObject>.ForCollectionItem(x=>x.Request.ToBudgetAccount, x => x.Amount).WithNumericRules(r => r.GreaterThan = 0),
        };
        public static List<ValidationRule> GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestRules = new()
        {
            RuleBuilder<GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject>.For(x => x.Request.BudgetAccount).Required(),
            RuleBuilder<GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject>.For(x => x.Request.BudgetAccount.Id).Required()
        };
        #endregion Budget
        #region Calendar
        public static List<ValidationRule> AddEventRequestRules = new()
        {
            RuleBuilder<AddEventRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent).Required(),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent.Title)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent.Description).Required()
                .WithStringRules(r => {
                    r.MaxLength = 500;
                }),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent.EventType).Required(),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent.StartDateTime).Required(),
            RuleBuilder<AddEventRequestObject>.For(x => x.Request.CalendarEvent.IsAllDay).Required()
        };
        #endregion Calendar
        #region Wallet
        public static List<ValidationRule> CreateWalletAccountRequestRules = new()
        {
            RuleBuilder<CreateWalletRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CreateWalletRequestObject>.For(x => x.Request.Wallet).Required(),
            RuleBuilder<CreateWalletRequestObject>.For(x => x.Request.Wallet.WalletName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<CreateWalletRequestObject>.For(x => x.Request.Wallet.Amount)
                .Required()
        };
        public static List<ValidationRule> GetWalletTransactionListRequestRules = new()
        {
            RuleBuilder<GetWalletTransactionListRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetWalletTransactionListRequestObject>.For(x => x.Request.Wallet).Required(),
            RuleBuilder<GetWalletTransactionListRequestObject>.For(x => x.Request.Wallet.Id).Required()
        };
        public static List<ValidationRule> CreateChangeWalletStatusRequestRules = new()
        {
            RuleBuilder<ChangeWalletStatusRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<ChangeWalletStatusRequestObject>.For(x => x.Request.Wallet).Required(),
            RuleBuilder<ChangeWalletStatusRequestObject>.For(x => x.Request.Wallet.Id).Required()
        };
        public static List<ValidationRule> DepositWalletAmountRequestObjectRules = new()
        {
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.Wallet).Required(),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.WalletTransaction).Required(),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.Wallet.Id).Required(),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.WalletTransaction.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.WalletTransaction.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.WalletTransaction.TransactionDate).Required(),
            RuleBuilder<DepositWalletAmountRequestObject>.For(x => x.Request.Account.IsFromAccount).Required(),
        };
        public static List<ValidationRule> WalletTransferRequestRules = new()
        {
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.Wallet).Required(),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.WalletTransaction).Required(),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.Wallet.Id).Required(),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.WalletTransaction.Amount)
                .Required()
                .WithNumericRules(r => {
                    r.GreaterThan = 0;
                }),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.WalletTransaction.Description)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.WalletTransaction.TransactionTag)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 1;
                    r.MaxLength = 100;
                }),
            RuleBuilder<WalletTransferRequestObject>.For(x => x.Request.WalletTransaction.TransactionDate).Required()
        };

        public static List<ValidationRule> GetWalletBalanceRequestRules = new()
        {
            RuleBuilder<GetWalletBalanceRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<GetWalletBalanceRequestObject>.For(x => x.Request.Account).Required(),
            RuleBuilder<GetWalletBalanceRequestObject>.For(x => x.Request.Account.Id).Required()
        };
        #endregion Wallet
        #region BulkTransaction
        //Add validation rules for BulkTransactionRequestObject and its nested properties as needed
        public static List<ValidationRule> BulkTransactionRequestRules = new()
        {
            RuleBuilder<BulkTransactionRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<BulkTransactionRequestObject>.For(x => x.Request).WithCollectionRules(r => {
                r.CollectionMinCount = 1;
            }),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Account).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.BudgetAccount).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Transaction).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.TransactionCategory).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Account.FromAccountId).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Account.ToAccountId).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Transaction.TransactionDate).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Transaction.TransactionTag).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Transaction.Amount).Required(),
            RuleBuilder<BulkTransactionRequestObject>.ForCollectionItem(x => x.Request, x => x.Transaction.Description).Required(),
        };
        #endregion BulkTransaction
    }
}
