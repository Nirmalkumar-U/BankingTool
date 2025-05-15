using BankingTool.Api.Filter;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class ReportController(IReportService reportService,
        IValidatorService validatorService) : HandleRequest(validatorService)
    {
        private readonly IReportService _reportService = reportService;

        //[HttpPost]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        //public Task<IActionResult> CreateAccount(CreateAccountRequestObject model)
        //{
        //    return HandleRequestAsync<CreateAccountRequestObject, bool>(
        //        model,
        //        ValidationRules.CreateAccountRequestRules,
        //        () => _bankAccountService.CreateAccount(model.Request)
        //    );
        //}
    }
}
