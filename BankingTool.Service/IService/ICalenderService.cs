using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface ICalenderService
    {
        Task<ResponseDto<List<CalendarEventDto>>> GetCalendarEventList();
    }
}
