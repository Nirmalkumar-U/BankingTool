using BankingTool.Model;
using BankingTool.Model.Dto.Response;

namespace BankingTool.Repository
{
    public interface IUserRepository
    {
        Task<(Users User, Role Role)> GetUserAndRoleByEmailId(string emailId);
        Task<(Users User, Role Role)> GetUserAndRoleByUserId(int userId);
        Task<List<int>> GetAllActionIdOfRole(int RoleId);
        Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId);
        Task<Customer> GetCustomerByUserId(int userId);
        Task<Staff> GetStaffByUserId(int userId);
        Task<List<UserListResponse>> GetUserList();
        int? CreateUser(Users userDetail, UserRole userRole, Staff staff, Customer customer, bool isCustomerNeedToInsert);
        Task<bool> IsUserExist(string email);
        int InsertUser(Users user);
        bool InsertUserRole(UserRole userRole);
        Task<List<GetActionsByUserIdDto>> GetActionsByUserId(int userId);
        bool InsertCustomer(Customer user);
        bool InsertStaff(Staff staff);
    }
}
