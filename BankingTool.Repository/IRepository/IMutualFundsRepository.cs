using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IMutualFundsRepository
    {
        Task<List<Scheme>> GetAllScheme();
        Task<Scheme> GetSchemeByCode(string schemeCode);
        Task<int?> AddScheme(Scheme scheme);
        Task<bool> SaveUpdateScheme(Scheme model);
        Task<MutualFunds> GetLastMutualFundsBySchemeId(int schemeId);
        Task<int?> AddMutualFunds(MutualFunds model);
        Task<bool> SaveUpdateMutualFunds(MutualFunds model);
        Task<List<UserInvestment>> GetAllUserInvestment();
        Task<List<UserInvestment>> GetUserInvestmentByUserId();
        Task<UserInvestment> GetUserInvestmentByInvestmentId(int investmentId);
        Task<UserInvestment> GetUserInvestmentBySchemeId(int schemeId);
        Task<int?> AddUserInvestment(UserInvestment model);
        Task<bool> SaveUpdateUserInvestment(UserInvestment model);
    }
}
