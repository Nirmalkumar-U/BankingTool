using BankingTool.Model;
using BankingTool.Repository.IRepository;

namespace BankingTool.Repository.Repository
{
    public class NotificationRepository(DataContext dataContext) : EntityRepository<Role>(dataContext), INotificationRepository
    {
    }
}
