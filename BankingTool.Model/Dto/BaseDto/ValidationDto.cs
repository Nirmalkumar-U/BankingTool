namespace BankingTool.Model.Dto.BaseDto
{
    public class ValidationRule
    {
        public string PropertyPath { get; set; }

        public bool? Required { get; set; }
        public bool? NotEmptyCollection { get; set; }

        public bool? ValueMatch { get; set; }
        public object ExpectedValue { get; set; }

        public Type EnumType { get; set; }

        public StringRules StringRules { get; set; }
        public NumericRules NumericRules { get; set; }
        public DateRules DateRules { get; set; }
        public CollectionRules CollectionRules { get; set; }

        public CustomRule CustomRule { get; set; }

        public string RuleId { get; set; }
    }

    public class StringRules
    {
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public int? BetweenMinLength { get; set; }
        public int? BetweenMaxLength { get; set; }
        public string RegexPattern { get; set; }
        public bool EmailFormat { get; set; }
        public bool NotEmpty { get; set; }
    }

    public class NumericRules
    {
        public double? GreaterThan { get; set; }
        public double? GreaterThanOrEqualTo { get; set; }
        public double? LessThan { get; set; }
        public double? LessThanOrEqualTo { get; set; }
        public double? EqualTo { get; set; }
        public double? NotEqualTo { get; set; }
        public double? RangeMin { get; set; }
        public double? RangeMax { get; set; }
        public double? BetweenMin { get; set; }
        public double? BetweenMax { get; set; }
    }

    public class DateRules
    {
        public DateTime? MustBeAfter { get; set; }
        public DateTime? MustBeBefore { get; set; }
        public DateTime? BetweenStartDate { get; set; }
        public DateTime? BetweenEndDate { get; set; }
    }

    public class CollectionRules
    {
        public int? CollectionMinCount { get; set; }
        public int? CollectionMaxCount { get; set; }
        public int? CollectionExactCount { get; set; }
        public bool? NoNullItems { get; set; }
        public bool? NoDuplicates { get; set; }
    }

    public class CustomRule
    {
        public string RuleKey { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ValidationResults
    {
        public string PropertyName { get; set; }
        public string PropertyPath { get; set; }
        public string ErrorMessage { get; set; }
    }
}
