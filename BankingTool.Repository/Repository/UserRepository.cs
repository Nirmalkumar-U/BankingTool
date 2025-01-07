using BankingTool.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository
{
    public class UserRepository(DataContext dataContext, IStoreProcedureRepository storeProcedureRepository) : EntityRepository<Users>(dataContext), IUserRepository
    {
        private readonly IStoreProcedureRepository _storeProcedureRepository = storeProcedureRepository;
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
        public async Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId)
        {
            return await dataContext.City.Where(x => x.StateId == stateId).Select(z => new DropDownDto
            {
                Key = z.CityId,
                Value = z.CityName
            }).ToListAsync();
        }
        public async Task<int> InsertUser(Users user)
        {
            Insert(user);
            return await Task.FromResult(user.UserId);
        }
        public async Task<bool> InsertUserRole(UserRole userRole)
        {
            return await Task.FromResult(Insert(userRole));
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
        public async Task<bool> InsertStaff(Staff staff)
        {
            return await Task.FromResult(Insert(staff));
        }
        public async Task<bool> InsertCustomer(Customer user)
        {
            return await Task.FromResult(Insert(user));
        }
    }
}
