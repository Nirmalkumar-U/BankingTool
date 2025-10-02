using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Dto.SPDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IBankAccountRepository
    {
        Task<List<DropDownDto>> GetBankDetailsDropDown();
        Task<List<DropDownDto>> GetAccountTypeDropDown();
        Task<List<DropDownDto>> GetAllActiveCustomerDropDown();
        Task<Account> GetLastAccount();
        int? InsertAccount(Account account);
        Task<bool> IsAnyAccountForThisCustomer(int customerId);
        Task<Customer> GetCustomerByCustomerId(int customerId);
        Task<List<DropDownDto>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId);
        Task<bool> IsCustomerHasCreditCardInThatBank(int customerId, int bankId);
        Task<List<GetTransactionsListResponseTransactionList>> GetTransactionByAccountId(int accountId, TransactionTagEnum? TransactionTag,
            DateTime? TransactionFromDate, DateTime? TransactionToDate, int? SenderAccountId, int? ReceiverAccountId, TransactionCategoryEnum? TransactionCategory);
        Task<GetTransactionsListResponseAccountInfo> GetAccountInfo(int accountId, string accountType, string name, string bankName);
        Task<List<GetTransactionsListResponseCardInfo>> GetCardInfoByAccountId(int accountId, string name, string bankName);
        Task<(int, string, string, string)> GetAccountIdByBankIdAndAccountTypeAndCustomerId(int bankId, int accountTypeId, int customerId);
        Task<List<DropDownDto>> GetBankDropDownListByCustomerId();
        Task<List<DropDownDto>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int BankId);
        Task<List<DropDownDto>> GetFromAccountListByCustomerId();
        Task<List<DropDownDto>> GetToAccountListOnWithoutCustomerId();
        Task<int> GetAccountBalance(int accountId);
        Task<Account> GetAccount(int AccountId);
        Task<Transaction> GetTransaction(int TransactionId);
        Task<List<DropDownDto>> GetToAccountListExcludedByAccountId(int accountId);
        Task<CreditScore> GetCreditScoreOfCustomer(int customerId);
        Task<List<DropDownDto>> GetAllTransactionTag();
        Task<List<DropDownDto>> GetAllTransactionCategory();
        Task<List<GetReceiverAccount>> GetReceiverAccountDropdown(int accountId);
        Task<List<GetSenderAccount>> GetSenderAccountDropdown(int accountId);
        Task<Transaction> GetLastTransaction(int AccountId);
        bool CreateAccount(Account account, Transaction transaction, TransactionDetail transactionDetail, Card debitCard, Card creditCard, CreditScore cardScore, Customer customer,
            bool CustomerWantCreditCard, bool IsAnyAccountForThisCustomer, bool IsUpdatePrimaryAccount);
        bool TransferAmount(Account fromAccount, Account toAccount, Transaction transaction, TransactionDetail fromTransactionDetail,
            TransactionDetail toTransactionDetail, List<BudgetTransactionDto> budgetTransactions, BudgetNotification budgetNotification, bool isCustomerAsBudgetAccount);
        bool CashTransfer(Account account, Transaction transaction, TransactionDetail transactionDetail, bool isBudgetAccountAttached, List<BudgetTransactionDto> budgetTransactions);
        Task<List<DashBoardAccountList>> GetDashBoardInitialLoad();
        Task<Card> GetLastCard(CardTypeEnum cardType);
        Task<List<DropDownDto>> GetAllAccountList();
    }
}
