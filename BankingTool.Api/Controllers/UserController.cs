using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Dto.Response;
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
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<LoggedInUserResponse>), 400)]
        public Task<IActionResult> Login(LoginRequestObject model)
        {
            return HandleRequestAsync<LoginRequestObject, LoggedInUserResponse>(
                model,
                ValidationRules.LoginRequestRules,
                () => _userService.Login(model.Request.User.Email, model.Request.User.Password)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TokenResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TokenResponse>), 400)]
        public Task<IActionResult> CreateToken(CreateTokenRequestObject model)
        {
            return HandleRequestAsync<CreateTokenRequestObject, TokenResponse>(
                model,
                ValidationRules.CreateTokenRequestRules,
                () => _userService.CreateToken(model.Request.User, model.Request.Role.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<UserInitialLoadResponse>), 400)]
        public Task<IActionResult> GetUserInitialLoad(GetUserInitialLoadRequestObject model)
        {
            return HandleRequestAsync<GetUserInitialLoadRequestObject, UserInitialLoadResponse>(
                model,
                ValidationRules.GetUserInitialLoadRequestRules,
                () => _userService.GetUserInitialLoad(model.Request.User.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetCityDropDownListByStateId(GetCityListRequestObject model)
        {
            return HandleRequestAsync<GetCityListRequestObject, bool>(
                model,
                ValidationRules.GetCityListRequestRules,
                () => _userService.GetCityDropDownListByStateId(model.Request.State.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int?>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int?>), 400)]
        public Task<IActionResult> SaveUser(SaveUserRequestObject model)
        {
            return HandleRequestAsync<SaveUserRequestObject, int?>(
                model,
                ValidationRules.SaveUserRequestRules,
                () => _userService.InsertUser(model)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<List<UserListResponse>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<UserListResponse>>), 400)]
        public Task<IActionResult> GetUserList()
        {
            return HandleRequestAsync<int?, List<UserListResponse>>(
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
