using BankingTool.Model;

namespace BankingTool.Repository
{
    public interface ICommonRepository
    {
        Task<List<DropDownDto>> GetAllStateDropDownList(); 
        Task SaveTransaction();
    }
}
