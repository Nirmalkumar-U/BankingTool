using BankingTool.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository
{
    public class RoleRepository(DataContext dataContext) : EntityRepository<Role>(dataContext), IRoleRepository
    {
        public async Task<List<DropDownDto>> GetRoleListDropDown()
        {
            return await dataContext.Role.Where(x => !x.IsDeleted).Select(z => new DropDownDto
            {
                Key = z.RoleId,
                Value = z.RoleCode + " | " + z.RoleName,
            }).ToListAsync();
        }
        public async Task<Role> GetRoleByRoleId(int roleId)
        {
            return await dataContext.Role.FirstOrDefaultAsync(x => !x.IsDeleted && x.RoleId == roleId);
        }
    }
}
