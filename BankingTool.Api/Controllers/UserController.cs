using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Dto.User;
using BankingTool.Service;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController(IEncriptDecriptService encriptDecriptService, IUserService userService,
        IValidatorService validatorService) : HandleRequest(validatorService)
    {
        private readonly IEncriptDecriptService _encriptDecriptService = encriptDecriptService;
        private readonly IUserService _userService = userService;

        [HttpGet]
        public IActionResult GetEncriptedString(string word)
        {
            var Result = _encriptDecriptService.EncryptData(word);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public IActionResult GetDecriptedString(string word)
        {
            var Result = _encriptDecriptService.DecryptData(word);
            return new OkObjectResult(Result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserDto>), 400)]
        public Task<IActionResult> Login(LoginRequestObject model)
        {
            return HandleRequestAsync<LoginRequestObject, LoggedInUserDto>(
                model,
                Ruleset.LoginRequestRules,
                () => _userService.Login(model.LoginRequest.User.Email, model.LoginRequest.User.Password)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 400)]
        public Task<IActionResult> CreateToken(CreateTokenRequestObject model)
        {
            return HandleRequestAsync<CreateTokenRequestObject, TokenDto>(
                model,
                Ruleset.CreateTokenRequestRules,
                () => _userService.CreateToken(model.CreateTokenRequest.User, model.CreateTokenRequest.Role.RoleId)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadDto>), 400)]
        public Task<IActionResult> GetUserInitialLoad(GetUserInitialLoadRequestObject model)
        {
            return HandleRequestAsync<GetUserInitialLoadRequestObject, UserInitialLoadDto>(
                model,
                Ruleset.CreateTokenRequestRules,//todo
                () => _userService.GetUserInitialLoad(model.GetUserInitialLoadRequest.User.UserId)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 400)]
        public Task<IActionResult> GetCityDropDownListByStateId(GetCityListRequestObject model)
        {
            return HandleRequestAsync<GetCityListRequestObject, bool?>(
                model,
                Ruleset.CreateTokenRequestRules,//todo
                () => _userService.GetCityDropDownListByStateId(model.GetCityListRequest.State.StateId)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 400)]
        public Task<IActionResult> SaveUser(SaveUserRequestObject model)
        {
            return HandleRequestAsync<SaveUserRequestObject, int?>(
                model,
                Ruleset.CreateTokenRequestRules,//todo
                () => _userService.InsertUser(model)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenDto>), 400)]
        public Task<IActionResult> GetUserList()
        {
            return HandleRequestAsync<int?, List<UserListDto>>(
                null,
                null,
                _userService.GetUserList
            );
        }
        [HttpGet]
        public async Task<IActionResult> Test(int userId)
        {
            var Result = await _userService.Test(userId);
            return new OkObjectResult(Result);
        }
    }
}
