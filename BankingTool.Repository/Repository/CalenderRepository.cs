using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class CalenderRepository(DataContext dataContext) : EntityRepository<CalendarEvent>(dataContext), ICalenderRepository
    {
        public async Task<List<CalendarEventDto>> GetCalendarEventList()
        {
            var calendarEventList = await dataContext.CalendarEvent
                .Where(x => x.UserId == dataContext.UserId && x.IsDeleted == false)
                .Select(x => new CalendarEventDto
                {
                    CalendarEventId = x.CalendarEventId,
                    UserId = x.UserId,
                    Title = x.Title,
                    Description = x.Description,
                    EventType = CalenderEventTypeEnum.MapCalendarEventType(x.EventType),
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    IsAllDay = x.IsAllDay,
                    Duration = x.Duration,
                    Recurrence = x.Recurrence
                }).ToListAsync();
            return calendarEventList;
        }
    }
}
