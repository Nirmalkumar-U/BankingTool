using System.Linq.Expressions;
using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Api.Validators
{
    public static class PropertyPathHelper
    {
        public static string GetPath<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            var path = expression.Body.ToString();
            var paramName = expression.Parameters[0].Name;
            return path.Replace($"{paramName}.", "");
        }
    }

    public static class RuleBuilder<T>
    {
        public static ValidationRule For<TProp>(Expression<Func<T, TProp>> expression)
        {
            return new ValidationRule
            {
                PropertyPath = PropertyPathHelper.GetPath(expression)
            };
        }
    }
    public static class RuleBuilderExtensions
    {
        public static ValidationRule Required(this ValidationRule rule)
        {
            rule.Required = true;
            return rule;
        }

        public static ValidationRule WithStringRules(this ValidationRule rule, Action<StringRules> setup)
        {
            rule.StringRules = new StringRules();
            setup(rule.StringRules);
            return rule;
        }
        public static ValidationRule WithNumericRules(this ValidationRule rule, Action<NumericRules> setup)
        {
            rule.NumericRules = new NumericRules();
            setup(rule.NumericRules);
            return rule;
        }
        public static ValidationRule WithDateRules(this ValidationRule rule, Action<DateRules> setup)
        {
            rule.DateRules = new DateRules();
            setup(rule.DateRules);
            return rule;
        }
        public static ValidationRule WithCollectionRules(this ValidationRule rule, Action<CollectionRules> setup)
        {
            rule.CollectionRules = new CollectionRules();
            setup(rule.CollectionRules);
            return rule;
        }
        public static ValidationRule WithCustomRule(this ValidationRule rule, Action<CustomRule> setup)
        {
            rule.CustomRule = new CustomRule();
            setup(rule.CustomRule);
            return rule;
        }
    }

}
