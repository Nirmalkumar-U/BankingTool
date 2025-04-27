using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.User
{
    public class GetCityListRequestObject
    {
        public GetCityListRequest Request { get; set; }
    }
    public class GetCityListRequest
    {
        public GetCityListRequestState State { get; set; }
    }
    public class GetCityListRequestState : RequestId
    {
    }
}
