using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.User;
using BankingTool.Model.Dto.Response;

namespace BankingTool.Service
{
    public interface IUserService
    {
        Task<ResponseDto<LoggedInUserResponse>> Login(string email, string password);
        Task<ResponseDto<TokenResponse>> CreateToken(CreateTokenRequestUser user, int roleId);
        Task<ResponseDto<UserInitialLoadResponse>> GetUserInitialLoad(int? userId);
        Task<ResponseDto<bool>> GetCityDropDownListByStateId(int stateId);
        Task<ResponseDto<List<UserListResponse>>> GetUserList();
        Task<ResponseDto<int?>> InsertUser(SaveUserRequestObject user);
        Task<List<GetActionsByUserIdDto>> Test(int id);
    }
}
