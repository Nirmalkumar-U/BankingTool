using BankingTool.Model;
using BankingTool.Model.Dto.Response;
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
        Task<List<GetTransactionsListResponseTransactionList>> GetTransactionByAccountId(int accountId);
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

        bool CreateAccount(Account account, Transaction transaction,TransactionDetail transactionDetail, Card debitCard, Card creditCard, CreditScore cardScore, Customer customer,
            bool CustomerWantCreditCard, bool IsAnyAccountForThisCustomer, bool IsUpdatePrimaryAccount);
        bool TransferAmount(Account fromAccount, Account toAccount, Transaction transaction, TransactionDetail fromTransactionDetail, TransactionDetail toTransactionDetail);
        void UpdateCustomer(Customer customer);
        int? InsertTransaction(Transaction transaction);
        Task<Card> GetLastCard(string cardType);
        int? InsertCard(Card card);
        int? InsertCreditScore(CreditScore creditScore);
    }
}
