using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;
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
        public async Task<ResponseDto<bool>> AcceptBudgetTransaction(int BudgetAccountId, int BudgetNotificationId)
        {
            var isAccepted = await _budgetRepository.AcceptBudgetTransaction(BudgetAccountId, BudgetNotificationId);
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
        public async Task<ResponseDto<GetBudgetListResponse>> GetBudgetList()
        {
            return new ResponseDto<GetBudgetListResponse>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = new GetBudgetListResponse
                {
                    budgetList = await _budgetRepository.GetBudgetList()
                },
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
                    BudgetStatus = budgetAccount.BudgetStatus,
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
            var budgetDropdownList = budgetList.Select(x => new DropDownDto
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
                }
            };
        }
        private async Task<string> GetNewBudgetNumber()
        {
            var lastBudgetAccount = await _budgetRepository.GetLastBudgetAccount();
            return ((lastBudgetAccount != null && long.TryParse(lastBudgetAccount.BudgetNumber, out long lastNumber)) ? lastNumber + 1 : 60000000).ToString();
        }
    }
}

