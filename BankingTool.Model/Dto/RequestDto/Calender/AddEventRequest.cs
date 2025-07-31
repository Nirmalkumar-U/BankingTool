using BankingTool.Model.Dto.InternalDto;

namespace BankingTool.Model.Dto.RequestDto.Calender
{
    public class AddEventRequestObject
    {
        public AddEventRequest Request { get; set; }
    }
    public class AddEventRequest
    {
        public CalendarEventDto CalendarEvent { get; set; }
    }
}
