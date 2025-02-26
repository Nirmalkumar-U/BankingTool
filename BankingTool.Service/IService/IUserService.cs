using BankingTool.Model;
using BankingTool.Model.Dto.User;

namespace BankingTool.Service
{
    public interface IUserService
    {
        Task<ResponseDto<LoggedInUserDto>> Login(string email, string password);
        Task<ResponseDto<TokenDto>> CreateToken(LoggedInUserDto user);
        Task<ResponseDto<UserInitialLoadDto>> GetUserInitialLoad(int? userId);
        Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId);
        Task<ResponseDto<List<UserListDto>>> GetUserList();
        Task<ResponseDto<int?>> InsertUser(SaveUserDto user);
        Task<List<GetActionsByUserIdDto>> Test(int id);
    }
}
