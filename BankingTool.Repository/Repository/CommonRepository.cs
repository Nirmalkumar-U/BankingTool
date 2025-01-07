using BankingTool.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository
{
    public class CommonRepository(DataContext dataContext) : EntityRepository<Users>(dataContext), ICommonRepository
    {
        public async Task<List<DropDownDto>> GetAllStateDropDownList()
        {
            return await dataContext.State.Where(z => !z.IsDeleted).Select(x => new DropDownDto { Key = x.StateId, Value = x.Abbreviation + " | " + x.StateName }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetAllCityDropDownList()
        {
            return await dataContext.City.Where(z => !z.IsDeleted).Select(x => new DropDownDto { Key = x.CityId, Value = x.CityName }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId)
        {
            return await dataContext.City.Where(z => !z.IsDeleted && z.StateId == stateId).Select(x => new DropDownDto { Key = x.CityId, Value = x.CityName }).ToListAsync();
        }
    }
}