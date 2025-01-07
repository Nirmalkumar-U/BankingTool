using BankingTool.Model;

namespace BankingTool.Repository
{
    public interface IUserRepository
    {
        Task<(Users User, Role Role)> GetUserAndRoleByEmailId(string emailId);
        Task<(Users User, Role Role)> GetUserAndRoleByUserId(int userId);
        Task<List<int>> GetAllActionIdOfRole(int RoleId);
        Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId);
        Task<int> InsertUser(Users user);
        Task<bool> InsertUserRole(UserRole userRole);
        Task<List<GetActionsByUserIdDto>> GetActionsByUserId(int userId);
        Task<bool> InsertCustomer(Customer user);
        Task<bool> InsertStaff(Staff staff);
    }
}
