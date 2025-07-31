using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICalenderRepository
    {
        Task<List<CalendarEventDto>> GetCalendarEventList();
        Task<int?> InsertEvent(CalendarEvent model);
    }
}
