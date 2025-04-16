using BankingTool.Model;
using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Dto.User;

namespace BankingTool.Service
{
    public interface IUserService
    {
        Task<ResponseDto<LoggedInUserDto>> Login(string email, string password);
        Task<ResponseDto<TokenDto>> CreateToken(CreateTokenRequestUser user, int roleId);
        Task<ResponseDto<UserInitialLoadDto>> GetUserInitialLoad(int? userId);
        Task<ResponseDto<bool?>> GetCityDropDownListByStateId(int stateId);
        Task<ResponseDto<List<UserListDto>>> GetUserList();
        Task<ResponseDto<int?>> InsertUser(SaveUserRequestObject user);
        Task<List<GetActionsByUserIdDto>> Test(int id);
    }
}
