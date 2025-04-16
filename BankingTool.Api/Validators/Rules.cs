using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Api.Validators
{
    public static class Ruleset
    {
        public static List<ValidationRule> LoginRequestRules = new List<ValidationRule>()
            {
                new ValidationRule()
                {
                    PropertyPath = "LoginRequest.User.Email",
                    StringRules = new StringRules
                    {
                        EmailFormat = true,
                        NotEmpty = true,
                    },
                    Required = true,
                },
                new ValidationRule()
                {
                    PropertyPath = "LoginRequest.User.Password",
                    StringRules = new StringRules
                    {
                        NotEmpty = true,
                        MinLength = 6,
                        MaxLength = 15,
                    },
                    Required = true,
                },
                new ValidationRule()
                {
                    PropertyPath = "LoginRequest.User",
                    Required = true,
                },
                new ValidationRule()
                {
                    PropertyPath = "LoginRequest",
                    Required = true,
                }
            };
        public static List<ValidationRule> CreateTokenRequestRules = new List<ValidationRule>()
        {
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.User",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.Role",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.User.UserId",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.User.FirstName",
                Required = true,
                StringRules = new StringRules
                    {
                        NotEmpty = true,
                        MaxLength = 20,
                    }
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.User.LastName",
                Required = true,
                StringRules = new StringRules
                    {
                        NotEmpty = true,
                        MaxLength = 20,
                    }
            },
            new ValidationRule()
            {
                PropertyPath = "CreateTokenRequest.User.Email",
                Required = true,
                StringRules = new StringRules
                {
                    EmailFormat = true,
                    NotEmpty = true,
                }
            },
            new ValidationRule
            {
                PropertyPath = "CreateTokenRequest.Role.RoleId",
                Required = true,
            }
        };
    }
}
