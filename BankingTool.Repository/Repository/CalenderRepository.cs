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
                    EventType = CalenderEventTypeEnum.MapCalendarEventTypeStringToEnum(x.EventType),
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    IsAllDay = x.IsAllDay,
                    Duration = x.Duration,
                    Recurrence = x.Recurrence
                }).ToListAsync();
            return calendarEventList;
        }

        public async Task<int?> InsertEvent(CalendarEvent model)
        {
            model.UserId = dataContext.UserId;
            return await Task.FromResult(InsertCalendarEvent(model));
        }
        private int? InsertCalendarEvent(CalendarEvent model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.CalendarEventId : null;
        }
    }
}
