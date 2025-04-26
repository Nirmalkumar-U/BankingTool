using BankingTool.Model.Dto.BaseDto;
using BankingTool.Service.IService;
using BankingTool.Service.Validator;

namespace BankingTool.Service.Service
{
    public class ValidatorService : IValidatorService
    {
        public (bool, List<ValidationResults>) Validate(object obj, List<ValidationRule> rules)
        {
            var validator = new DataValidator();
            var (isValid, validationResult) = validator.Validate(obj, rules);
            return (isValid, validationResult);
        }
    }
}
