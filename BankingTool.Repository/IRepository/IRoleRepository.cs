using BankingTool.Model;

namespace BankingTool.Repository
{
    public interface IRoleRepository
    {
        Task<List<DropDownDto>> GetRoleListDropDown();
        Task<Role> GetRoleByRoleId(int roleId);
    }
}
