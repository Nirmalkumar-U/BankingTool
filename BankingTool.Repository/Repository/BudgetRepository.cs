using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;

namespace BankingTool.Repository.Repository
{
    public class BudgetRepository(DataContext dataContext) : EntityRepository<BudgetAccount>(dataContext), IBudgetRepository
    {
    }
}
