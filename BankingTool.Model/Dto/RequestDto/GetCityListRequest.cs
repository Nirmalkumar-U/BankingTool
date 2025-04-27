namespace BankingTool.Model.Dto.RequestDtos
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
