using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.User
{
    public class GetUserInitialLoadRequestObject
    {
        public GetUserInitialLoadRequest Request { get; set; }
    }
    public class GetUserInitialLoadRequest
    {
        public GetUserInitialLoadRequestUser User { get; set; }
    }
    public class GetUserInitialLoadRequestUser : RequestNullableId
    {
    }

}
