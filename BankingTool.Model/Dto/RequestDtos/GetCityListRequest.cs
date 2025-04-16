namespace BankingTool.Model.Dto.RequestDtos
{
    public class GetCityListRequestObject
    {
        public GetCityListRequest GetCityListRequest { get; set; }
    }
    public class GetCityListRequest
    {
        public GetCityListRequestState State { get; set; }
    }
    public class GetCityListRequestState
    {
        public int StateId { get; set; }
    }
}
