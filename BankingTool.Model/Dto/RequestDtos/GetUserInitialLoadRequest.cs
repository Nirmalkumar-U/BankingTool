namespace BankingTool.Model.Dto.RequestDtos
{
    public class GetUserInitialLoadRequestObject
    {
        public GetUserInitialLoadRequest GetUserInitialLoadRequest { get; set; }
    }
    public class GetUserInitialLoadRequest
    {
        public GetUserInitialLoadRequestUser User { get; set; }
    }
    public class GetUserInitialLoadRequestUser
    {
        public int? UserId { get; set; }
    }

}
