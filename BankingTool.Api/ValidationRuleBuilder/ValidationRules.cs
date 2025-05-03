using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.RequestDto.User;

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
        };
        #endregion BankAccountController
    }
}
