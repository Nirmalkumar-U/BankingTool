using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;

namespace BankingTool.Repository.Repository
{
    public class ReportRepository(DataContext dataContext) : EntityRepository<TransactionDetail>(dataContext), IReportRepository
    {
    }
}
