using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BudgetService(IBudgetRepository budgetRepository) : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
    }
}

