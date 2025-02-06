using BankingTool.Model;

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
        void UpdateCustomer(Customer customer);
        int? InsertTransaction(Transaction transaction);
        Task<Card> GetLastCard(string cardType);
        int? InsertCard(Card card);
        int? InsertCreditScore(CreditScore creditScore);
    }
}
