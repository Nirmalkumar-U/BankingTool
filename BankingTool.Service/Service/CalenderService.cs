using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
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
    }
}

