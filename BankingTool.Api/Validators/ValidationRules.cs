using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Api.Validators
{
    public class ValidationRules
    {
        public static List<ValidationRule> LoginRequestRules = new()
        {
            RuleBuilder<LoginRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User).Required(),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User.Email)
                .Required()
                .WithStringRules(r => {
                    r.EmailFormat = true;
                    r.NotEmpty = true;
                }),
            RuleBuilder<LoginRequestObject>.For(x => x.Request.User.Password)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                    r.MinLength = 6;
                    r.MaxLength = 15;
                }),
        };
        public static List<ValidationRule> CreateTokenRequestRules = new()
        {
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.Role).Required(),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.Email)
                .Required()
                .WithStringRules(r => {
                    r.EmailFormat = true;
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.FirstName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.User.LastName)
                .Required()
                .WithStringRules(r => {
                    r.NotEmpty = true;
                }),
            RuleBuilder<CreateTokenRequestObject>.For(x => x.Request.Role.Id).Required(),
        };

    }
}
