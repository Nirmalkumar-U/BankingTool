using BankingTool.Model.Dto.InternalDto;

namespace BankingTool.Repository.IRepository
{
    public interface ICalenderRepository
    {
        Task<List<CalendarEventDto>> GetCalendarEventList();
    }
}
