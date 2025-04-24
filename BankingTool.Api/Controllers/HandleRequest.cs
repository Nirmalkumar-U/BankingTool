using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Repository;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    public class HandleRequest(IValidatorService validatorService) : ControllerBase
    {

        private readonly IValidatorService _validatorService = validatorService;
        public async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(
                TRequest model,
                List<ValidationRule> ruleSet,
                Func<Task<ResponseDto<TResponse>>> serviceMethod)
        {
            var errors = new List<Errors>();
            ResponseDto<TResponse> result = new()
            {
                Status = false,
                Errors = null,
                ValidationErrors = null,
                DropDownList = null
            };
            if (model is not null && ruleSet?.Any() == true)
            {
                var (isValid, validationResult) = _validatorService.Validate(model, ruleSet);

                result.ValidationErrors = [.. validationResult];
                if (!isValid)
                {
                    result.StatuCode = 400;
                    result.Message = ErrorMessage.ValidationFailed;
                    return BadRequest(result);
                }
            }

            try
            {
                result = await serviceMethod();
            }
            catch (Exception ex)
            {
                errors.Add(new Errors
                {
                    ErrorMessage = ex.Message,
                    PropertyName = serviceMethod.Method.Name
                });
            }

            result.Errors = [.. (errors ?? []), .. (result.Errors ?? [])];

            if (result.Errors.Count > 0)
            {
                result.Message = ErrorMessage.InternalServerError;
                result.StatuCode = 400;
                return BadRequest(result);
            }

            result.StatuCode = 200;
            return Ok(result);
        }

    }
}
