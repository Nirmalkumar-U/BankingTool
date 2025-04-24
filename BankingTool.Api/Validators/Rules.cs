using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Api.Validators
{
    public static class Ruleset
    {
        public static List<ValidationRule> LoginRequestRules = new List<ValidationRule>()
            {
                new ValidationRule()
                {
                    PropertyPath = "Request.User.Email",
                    StringRules = new StringRules
                    {
                        EmailFormat = true,
                        NotEmpty = true,
                    },
                    Required = true,
                },
                new ValidationRule()
                {
                    PropertyPath = "Request.User.Password",
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
                    PropertyPath = "Request.User",
                    Required = true,
                },
                new ValidationRule()
                {
                    PropertyPath = "Request",
                    Required = true,
                }
            };
        public static List<ValidationRule> CreateTokenRequestRules = new List<ValidationRule>()
        {
            new ValidationRule
            {
                PropertyPath = "Request",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.User",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.Role",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.User.Id",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.User.FirstName",
                Required = true,
                StringRules = new StringRules
                    {
                        NotEmpty = true,
                        MaxLength = 20,
                    }
            },
            new ValidationRule
            {
                PropertyPath = "Request.User.LastName",
                Required = true,
                StringRules = new StringRules
                    {
                        NotEmpty = true,
                        MaxLength = 20,
                    }
            },
            new ValidationRule()
            {
                PropertyPath = "Request.User.Email",
                Required = true,
                StringRules = new StringRules
                {
                    EmailFormat = true,
                    NotEmpty = true,
                }
            },
            new ValidationRule
            {
                PropertyPath = "Request.Role.Id",
                Required = true,
            }
        };
        public static List<ValidationRule> GetUserInitialLoadRequestRules = new List<ValidationRule>()
        {
            new ValidationRule
            {
                PropertyPath = "Request",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.User",
                Required = true,
            }
        };
        public static List<ValidationRule> GetCityListRequestRules = new List<ValidationRule>()
        {
            new ValidationRule
            {
                PropertyPath = "Request",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.State",
                Required = true,
            },
            new ValidationRule
            {
                PropertyPath = "Request.State.Id",
                Required = true,
            }
        };
    }
}
