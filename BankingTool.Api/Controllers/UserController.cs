using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.User;
using BankingTool.Model.Dto.Response;
using BankingTool.Service;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController(IEncriptDecriptService encriptDecriptService, IUserService userService,
        IValidatorService validatorService, ICacheProvider cacheProvider) : HandleRequest(validatorService, cacheProvider)
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
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserResponse>), 400)]
        public async Task<IActionResult> Login(LoginRequestObject model)
        {
            return await HandleRequestAsync<LoginRequestObject, LoggedInUserResponse>(
                model,
                ValidationRules.LoginRequestRules,
                () => _userService.Login(model.Request.User.Email, model.Request.User.Password)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenResponse>), 400)]
        public async Task<IActionResult> CreateToken(CreateTokenRequestObject model)
        {
            return await HandleRequestAsync<CreateTokenRequestObject, TokenResponse>(
                model,
                ValidationRules.CreateTokenRequestRules,
                () => _userService.CreateToken(model.Request.User, model.Request.Role.Id)
            );
        }

        [HttpPost]
        [TokenAuthorization]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadResponse>), 400)]
        public async Task<IActionResult> GetUserInitialLoad(GetUserInitialLoadRequestObject model)
        {
            return await HandleRequestAsync<GetUserInitialLoadRequestObject, UserInitialLoadResponse>(
                model,
                ValidationRules.GetUserInitialLoadRequestRules,
                () => _userService.GetUserInitialLoad(model.Request.User.Id),
                true
            );
        }

        [HttpPost]
        [TokenAuthorization]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetCityDropDownListByStateId(GetCityListRequestObject model)
        {
            return await HandleRequestAsync<GetCityListRequestObject, bool>(
                model,
                ValidationRules.GetCityListRequestRules,
                () => _userService.GetCityDropDownListByStateId(model.Request.State.Id),
                true
            );
        }

        [HttpPost]
        [TokenAuthorization]
        [ProducesResponseType(typeof(ResponseDto<int?>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int?>), 400)]
        public async Task<IActionResult> SaveUser(SaveUserRequestObject model)
        {
            return await HandleRequestAsync<SaveUserRequestObject, int?>(
                model,
                ValidationRules.SaveUserRequestRules,
                () => _userService.InsertUser(model)
            );
        }
        [HttpGet]
        [TokenAuthorization]
        [ProducesResponseType(typeof(ResponseDto<List<UserListResponse>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<UserListResponse>>), 400)]
        public async Task<IActionResult> GetUserList()
        {
            return await HandleRequestAsync<int?, List<UserListResponse>>(
                null,
                null,
                _userService.GetUserList,
                true
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
