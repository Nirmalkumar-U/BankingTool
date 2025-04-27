using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository
{
    public class UserRepository(DataContext dataContext) : EntityRepository<Users>(dataContext), IUserRepository
    {
        #region Get
        public async Task<(Users User, Role Role)> GetUserAndRoleByEmailId(string emailId)
        {
            var result = await (from u in dataContext.Users.AsNoTracking()
                                join ur in dataContext.UserRole.AsNoTracking() on u.UserId equals ur.UserId
                                join r in dataContext.Role.AsNoTracking() on ur.RoleId equals r.RoleId
                                where u.EmailId == emailId && u.IsActive == true
                                select new
                                {
                                    user = u,
                                    role = r
                                }).FirstOrDefaultAsync();
            if (result == null)
                return (null, null);
            return (result.user, result.role);
        }
        public async Task<(Users User, Role Role)> GetUserAndRoleByUserId(int userId)
        {
            var result = await (from u in dataContext.Users.AsNoTracking()
                                join ur in dataContext.UserRole.AsNoTracking() on u.UserId equals ur.UserId
                                join r in dataContext.Role.AsNoTracking() on ur.RoleId equals r.RoleId
                                where u.UserId == userId && u.IsActive == true
                                select new
                                {
                                    user = u,
                                    role = r
                                }).FirstOrDefaultAsync();
            if (result == null)
                return (null, null);
            return (result.user, result.role);
        }
        public async Task<List<int>> GetAllActionIdOfRole(int RoleId)
        {
            return await dataContext.RoleAccess.Where(x => x.RoleId == RoleId).Select(z => z.ActionId).ToListAsync();
        }
        public async Task<Customer> GetCustomerByUserId(int userId)
        {
            return await dataContext.Customer.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<Staff> GetStaffByUserId(int userId)
        {
            return await dataContext.Staff.FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId)
        {
            return await dataContext.City.Where(x => x.StateId == stateId).Select(z => new DropDownDto
            {
                Key = z.CityId,
                Value = z.CityName
            }).ToListAsync();
        }
        public async Task<List<GetActionsByUserIdDto>> GetActionsByUserId(int userId)
        {
            var sql = "EXEC GetActionsByUserId @UserId";
            //var sqlQuery = "EXEC GetActionsByUserId @UserId, @StartDate, @EndDate";
            var parameters = new[]
                            {
                                new Microsoft.Data.SqlClient.SqlParameter("@UserId", userId.ToString())
                                //new Microsoft.Data.SqlClient.SqlParameter("@StartDate", startDate),
                                //new Microsoft.Data.SqlClient.SqlParameter("@EndDate", endDate)
                            };

            var actions = await dataContext.Set<GetActionsByUserIdDto>()
                .FromSqlRaw(sql, parameters)
                .ToListAsync();

            return actions;
        }
        public async Task<List<UserListResponse>> GetUserList()
        {
            return await (from u in dataContext.Users.AsNoTracking()
                          join ur in dataContext.UserRole.AsNoTracking() on u.UserId equals ur.UserId
                          join r in dataContext.Role.AsNoTracking() on ur.RoleId equals r.RoleId
                          join c in dataContext.Customer.AsNoTracking() on u.UserId equals c.UserId
                          join s in dataContext.State on u.State equals s.StateId
                          join ci in dataContext.City on u.City equals ci.CityId
                          where u.IsDeleted == false
                          select new UserListResponse
                          {
                              UserId = u.UserId,
                              UserName = u.FirstName + " " + u.LastName,
                              UserMailId = u.EmailId,
                              IsActive = u.IsActive,
                              Password = u.Password,
                              PrimaryAccountNumber = c.PrimaryAccountNumber,
                              RoleName = r.RoleName,
                              State = s.StateName,
                              City = ci.CityName
                          }).ToListAsync();
        }
        #endregion GET

        #region Saving
        public int? CreateUser(Users userDetail, UserRole userRole, Staff staff, Customer customer, bool isCustomerNeedToInsert)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                int? userId = InsertUser(userDetail);
                userRole.UserId = userId.Value;
                _ = InsertUserRole(userRole);
                if (isCustomerNeedToInsert)
                {
                    customer.UserId = userId.Value;
                    InsertCustomer(customer);
                }
                else
                {
                    staff.UserId = userId.Value;
                    InsertStaff(staff);
                }
                sqlTransaction.Commit();
                return userId.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return null;
            }
        }
        public int InsertUser(Users user)
        {
            Insert(user);
            return user.UserId;
        }
        public bool InsertUserRole(UserRole userRole)
        {
            return Insert(userRole);
        }
        public bool InsertStaff(Staff staff)
        {
            return Insert(staff);
        }
        public bool InsertCustomer(Customer user)
        {
            return Insert(user);
        }
        #endregion Saving
    }
}
