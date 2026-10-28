using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class MutualFundsRepository(DataContext dataContext) : EntityRepository<MutualFunds>(dataContext), IMutualFundsRepository
    {
        public async Task<List<Scheme>> GetAllScheme()
        {
            return await dataContext.Scheme.Include(x => x.MutualFunds).ToListAsync();
        }

        public async Task<Scheme> GetSchemeByCode(string schemeCode)
        {
            return await dataContext.Scheme.FirstOrDefaultAsync(x => x.SchemeCode == schemeCode);
        }

        public async Task<int?> AddScheme(Scheme scheme)
        {
            return await Task.FromResult(InsertScheme(scheme));
        }

        public async Task<bool> SaveUpdateScheme(Scheme model)
        {
            return await Task.FromResult(UpdateScheme(model));
        }

        public async Task<MutualFunds> GetLastMutualFundsBySchemeId(int schemeId)
        {
            return await dataContext.MutualFunds.OrderByDescending(x => x.MutualFundDate).FirstOrDefaultAsync(x => x.SchemeId == schemeId);
        }
        public async Task<int?> AddMutualFunds(MutualFunds model)
        {
            return await Task.FromResult(InsertMutualFunds(model));
        }

        public async Task<bool> SaveUpdateMutualFunds(MutualFunds model)
        {
            return await Task.FromResult(UpdateMutualFunds(model));
        }

        public async Task<List<UserInvestment>> GetAllUserInvestment()
        {
            return await dataContext.UserInvestment.ToListAsync();
        }

        public async Task<List<UserInvestment>> GetUserInvestmentByUserId()
        {
            return await dataContext.UserInvestment.Include(x => x.Scheme).ThenInclude(z => z.MutualFunds).Where(x => x.UserId == dataContext.UserId).ToListAsync();
        }

        public async Task<UserInvestment> GetUserInvestmentByInvestmentId(int investmentId)
        {
            return await dataContext.UserInvestment.FirstOrDefaultAsync(x => x.InvestmentId == investmentId);
        }

        public async Task<UserInvestment> GetUserInvestmentBySchemeId(int schemeId)
        {
            return await dataContext.UserInvestment.FirstOrDefaultAsync(x => x.SchemeId == schemeId);
        }

        public async Task<int?> AddUserInvestment(UserInvestment model)
        {
            model.UserId = dataContext.UserId;
            return await Task.FromResult(InsertUserInvestment(model));
        }

        public async Task<bool> SaveUpdateUserInvestment(UserInvestment model)
        {
            return await Task.FromResult(UpdateUserInvestment(model));
        }

        private int? InsertScheme(Scheme model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.SchemeId : null;
        }
        private bool UpdateScheme(Scheme model)
        {
            Update(model);
            return true;
        }

        private int? InsertMutualFunds(MutualFunds model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.MutualFundId : null;
        }
        private bool UpdateMutualFunds(MutualFunds model)
        {
            Update(model);
            return true;
        }

        private int? InsertUserInvestment(UserInvestment model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.InvestmentId : null;
        }
        private bool UpdateUserInvestment(UserInvestment model)
        {
            Update(model);
            return true;
        }
    }
}
