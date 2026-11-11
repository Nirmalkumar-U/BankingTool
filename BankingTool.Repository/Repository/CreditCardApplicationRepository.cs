using BankingTool.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CreditCardApplicationRepository(DataContext dataContext) : EntityRepository<CreditCardApplication>(dataContext), ICreditCardApplicationRepository
    {
        public async Task<CreditCardApplication> GetApplicationById(int applicationId)
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
        }

        public async Task<List<CreditCardApplication>> GetApplicationsByCustomerId(int customerId)
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .Where(x => x.AccountId == customerId)
                .OrderByDescending(x => x.ApplicationDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardApplication>> GetApplicationsByStatus(CreditCardApplicationStatus status)
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.ApplicationDate)
                .ToListAsync();
        }

        public async Task<List<CreditCardApplication>> GetPendingApplications()
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .Where(x => x.Status == CreditCardApplicationStatus.Pending)
                .OrderByDescending(x => x.ApplicationDate)
                .ToListAsync();
        }

        public async Task<int> InsertApplication(CreditCardApplication application)
        {
            application.CreatedDate = DateTime.UtcNow;
            application.ModifiedDate = DateTime.UtcNow;
            application.ApplicationDate = DateTime.UtcNow;
            application.CreatedBy = dataContext.UserName;
            dataContext.CreditCardApplication.Add(application);
            await dataContext.SaveChangesAsync();
            return application.ApplicationId;
        }

        public async Task<bool> UpdateApplication(CreditCardApplication application)
        {
            application.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardApplication.Update(application);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ApproveApplication(int applicationId, decimal approvedLimit, string approvedBy, string remarks)
        {
            var application = await dataContext.CreditCardApplication.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
            if (application == null) return false;

            application.Status = CreditCardApplicationStatus.Approved;
            application.ApprovedLimit = approvedLimit;
            application.ApprovedBy = approvedBy;
            application.DecisionRemarks = remarks;
            application.ApprovedDate = DateTime.UtcNow;
            application.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardApplication.Update(application);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RejectApplication(int applicationId, string approvalBy, string remarks)
        {
            var application = await dataContext.CreditCardApplication.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
            if (application == null) return false;

            application.Status = CreditCardApplicationStatus.Rejected;
            application.ApprovedBy = approvalBy;
            application.DecisionRemarks = remarks;
            application.ApprovedDate = DateTime.UtcNow;
            application.ModifiedDate = DateTime.UtcNow;
            dataContext.CreditCardApplication.Update(application);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<CreditCardApplication>> GetAllApplications()
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .OrderByDescending(x => x.ApplicationDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteApplication(int applicationId)
        {
            var application = await dataContext.CreditCardApplication.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
            if (application == null) return false;

            dataContext.CreditCardApplication.Remove(application);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<CreditCardApplication> GetLatestApplicationByCustomerId(int customerId)
        {
            return await dataContext.CreditCardApplication
                .AsNoTracking()
                .Where(x => x.AccountId == customerId)
                .OrderByDescending(x => x.ApplicationDate)
                .FirstOrDefaultAsync();
        }
    }
}
