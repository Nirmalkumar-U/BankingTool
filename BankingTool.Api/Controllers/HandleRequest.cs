using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Repository;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BankingTool.Api.Controllers
{
    public class HandleRequest(IValidatorService validatorService, ICacheProvider cacheProvider) : ControllerBase
    {
        private readonly IValidatorService _validatorService = validatorService;
        private readonly ICacheProvider _cacheProvider = cacheProvider;

        public async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(
                TRequest model,
                List<ValidationRule> ruleSet,
                Func<Task<ResponseDto<TResponse>>> serviceMethod,
                bool addCache = false)
        {
            var errors = new List<Errors>();
            ResponseDto<TResponse> result = new()
            {
                Status = false,
                Errors = [],
                ValidationErrors = [],
                DropDownList = []
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
                string cacheKey = $"{serviceMethod.Method.Name}:{System.Text.Json.JsonSerializer.Serialize(model)}";
                if (addCache)
                {
                    if (_cacheProvider.TryGetValue(cacheKey, out ResponseDto<TResponse> cached) && cached is not null)
                    {
                        return Ok(cached);
                    }

                    result = await serviceMethod();
                    if (result.Status && result.Response is not null)
                    {
                        var cacheOption = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                            SlidingExpiration = TimeSpan.FromMinutes(5),
                            Size = 5096
                        };
                        _cacheProvider.Set(cacheKey, result, cacheOption);
                    }
                }
                else
                {
                    result = await serviceMethod();
                }
            }
            catch (Exception ex)
            {
                errors.Add(new Errors
                {
                    ErrorMessage = ex.Message,
                    PropertyName = serviceMethod.Method.Name
                });
            }

            if (result.Errors.Count > 0)
            {
                result.Errors = [.. (errors ?? []), .. (result.Errors ?? [])];
                result.Message = ErrorMessage.BadRequest;
                result.StatuCode = 400;
                return BadRequest(result);
            }
            result.Errors = [.. (errors ?? []), .. (result.Errors ?? [])];

            if (result.StatuCode == 500 || errors.Any())
            {
                result.Message = result.Message ?? ErrorMessage.InternalServerError;
                return StatusCode(500, result);
            }
            else if (result.StatuCode == 404)
            {
                result.Message = result.Message ?? ErrorMessage.NotFound;
                return NotFound(result);
            }
            else if (result.StatuCode == 200)
            {
                result.Message = result.Message ?? ErrorMessage.Success;
                return Ok(result);
            }
            else if (result.StatuCode == 201)
            {
                result.Message = result.Message ?? ErrorMessage.Created;
                return CreatedAtAction(serviceMethod.Method.Name, result);
            }
            else if (result.StatuCode == 204)
            {
                return NoContent();
            }
            else if (result.StatuCode == 207)
            {
                result.Message = result.Message ?? ErrorMessage.MultiStatus;
                return StatusCode(207, result); ;
            }
            else
            {
                result.Message = result.Message ?? ErrorMessage.Success;
                return Ok(result);
            }
        }
    }
}
