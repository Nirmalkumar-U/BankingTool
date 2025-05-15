using BankingTool.Model;
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
                BudgetStatus = "Active"
            };
            BudgetTransaction budgetTransaction = new()
            {
                Amount = amount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCatagory.Deposit,
                TransactionTag = TransactionTag.Deposit
            };
            BudgetTransactionDetail budgetTransactionDetails = new()
            {
                TransactionType = TransactionType.Credit,
                StageBalance = amount,
                TransactionRole = TransactionRole.Receiver,
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


        private async Task<string> GetNewBudgetNumber()
        {
            var lastBudgetAccount = await _budgetRepository.GetLastBudgetAccount();
            return ((lastBudgetAccount != null && long.TryParse(lastBudgetAccount.BudgetNumber, out long lastNumber)) ? lastNumber + 1 : 60000000).ToString();
        }
    }
}

