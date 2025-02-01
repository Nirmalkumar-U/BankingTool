using BankingTool.Model;
using BankingTool.Service;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(IEncriptDecriptService encriptDecriptService, IUserService userService) : ControllerBase
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
        [HttpGet]
        public async Task<IActionResult> Login(string email, string password)
        {
            var Result = await _userService.Login(email, password);
            return new OkObjectResult(Result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoggedInUserDto user)
        {
            var Result = await _userService.CreateToken(user);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInitialLoad(int? userId)
        {
            var Result = await _userService.GetUserInitialLoad(userId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCityDropDownListByStateId(int stateId)
        {
            var Result = await _userService.GetCityDropDownListByStateId(stateId);
            return new OkObjectResult(Result);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUser(SaveUserDto user)
        {
            var Result = await _userService.InsertUser(user);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> Test(int userId)
        {
            var Result = await _userService.Test(userId);
            return new OkObjectResult(Result);
        }
    }
}
