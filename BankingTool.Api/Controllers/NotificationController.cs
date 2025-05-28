using BankingTool.Api.Filter;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class NotificationController(INotificationService notificationService,
        IValidatorService validatorService, ICacheProvider cacheProvider) : HandleRequest(validatorService, cacheProvider)
    {
        private readonly INotificationService _notificationService = notificationService;

        //[HttpPost]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        //public Task<IActionResult> CreateAccount(CreateAccountRequestObject model)
        //{
        //    return HandleRequestAsync<CreateAccountRequestObject, bool>(
        //        model,
        //        ValidationRules.CreateAccountRequestRules,
        //        () => _notificationService.CreateAccount(model.Request)
        //    );
        //}
    }
}
