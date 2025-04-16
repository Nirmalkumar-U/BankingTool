using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Service.IService
{
    public interface IValidatorService
    {
        public (bool, List<ValidationResults>) Validate(object obj, List<ValidationRule> rules);
    }
}
