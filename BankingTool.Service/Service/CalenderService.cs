using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class CalenderService(ICalenderRepository calenderRepository) : ICalenderService
    {
        private readonly ICalenderRepository _calenderRepository = calenderRepository;

        public async Task<ResponseDto<List<CalendarEventDto>>> GetCalendarEventList()
        {
            return new ResponseDto<List<CalendarEventDto>>()
            {
                Errors = [],
                ValidationErrors = [],
                Response = await _calenderRepository.GetCalendarEventList(),
                Status = true,
                StatuCode = 200
            };
        }
        public async Task<ResponseDto<int?>> AddEvent(CalendarEventDto model)
        {
            CalendarEvent calendarEvent = new CalendarEvent
            {
                Title = model.Title,
                Description = model.Description,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Duration = model.Duration,
                EventType = model.EventType.CalendarEventTypeEnumToString(),
                IsAllDay = model.IsAllDay,
                IsPublic = true,
                Recurrence = model.Recurrence,
                Priority = "Normal"
            };
            var calenderEventId = await _calenderRepository.InsertEvent(calendarEvent);
            return new ResponseDto<int?>()
            {
                Errors = calenderEventId == null ? [new Errors { ErrorMessage = "Failed to save event.", PropertyName = "CalendarEvent" }] : [],
                ValidationErrors = [],
                Response = calenderEventId,
                Status = calenderEventId != null,
                StatuCode = calenderEventId == null ? 400 : 200
            };
        }
    }
}

