namespace BankingTool.Model.Dto.RequestDtos
{
    public class GetUserInitialLoadRequestObject
    {
        public GetUserInitialLoadRequest Request { get; set; }
    }
    public class GetUserInitialLoadRequest
    {
        public GetUserInitialLoadRequestUser User { get; set; }
    }
    public class GetUserInitialLoadRequestUser : RequestId
    {
        public GetUserInitialLoadRequestUser() : base(true)
        {
        }
    }

}
