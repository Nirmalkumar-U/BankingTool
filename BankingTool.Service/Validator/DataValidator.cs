using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using BankingTool.Model.Dto.BaseDto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankingTool.Service.Validator
{
    public static class CustomValidators
    {
        public static readonly Dictionary<string, Func<object, bool>> Rules = new()
        {
            ["IsPositiveInteger"] = val => val is int i && i > 0,
            ["NoSpecialCharacters"] = val => val is string s && Regex.IsMatch(s, @"^[a-zA-Z0-9\s]+$"),
            ["FutureDateOnly"] = val => val is DateTime dt && dt > DateTime.Now
        };
    }

    public class DataValidator
    {
        public (bool, List<ValidationResults>) Validate(object obj, List<ValidationRule> rules)
        {
            List<ValidationResults> results = [];

            foreach (var rule in rules)
            {
                var paths = ExpandWildcardPaths(obj, rule.PropertyPath);

                foreach (var path in paths)
                {
                    var value = GetPropertyValue(obj, path);

                    string propertyName = path.Substring(path.LastIndexOf('.') + 1);

                    if (rule.Required == true && value == null)
                    {
                        results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} is required." });
                        continue;
                    }

                    if (rule.ValueMatch == true && !Equals(value, rule.ExpectedValue))
                    {
                        results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} should be equal to {rule.ExpectedValue}." });
                    }

                    if (rule.EnumType != null && value != null && !Enum.IsDefined(rule.EnumType, value))
                    {
                        results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} is not a valid {rule.EnumType.Name}." });
                    }

                    if (rule.StringRules != null && value is string strVal)
                    {
                        if (rule.StringRules.MinLength.HasValue && strVal.Length < rule.StringRules.MinLength)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be at least {rule.StringRules.MinLength} characters." });
                        if (rule.StringRules.MaxLength.HasValue && strVal.Length > rule.StringRules.MaxLength)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be at most {rule.StringRules.MaxLength} characters." });

                        if (rule.StringRules.BetweenMinLength.HasValue && rule.StringRules.BetweenMaxLength.HasValue &&
                            (strVal.Length < rule.StringRules.BetweenMinLength || strVal.Length > rule.StringRules.BetweenMaxLength))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} length must be between {rule.StringRules.BetweenMinLength} and {rule.StringRules.BetweenMaxLength}." });

                        if (!string.IsNullOrWhiteSpace(rule.StringRules.RegexPattern) && !Regex.IsMatch(strVal, rule.StringRules.RegexPattern))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} does not match the required format." });

                        if (rule.StringRules.EmailFormat && !new EmailAddressAttribute().IsValid(strVal))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be a valid email address." });
                        if (rule.StringRules.NotEmpty && strVal == "")
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} is required." });

                    }

                    if (rule.NumericRules != null && value is IComparable comp && rule.NumericRules.GetType().GetProperties().Any(p => p.GetValue(rule.NumericRules) != null))
                    {
                        var valueType = value.GetType();

                        object ConvertRuleValue(object ruleValue)
                        {
                            try
                            {
                                return Convert.ChangeType(ruleValue, valueType);
                            }
                            catch
                            {
                                return null;
                            }
                        }

                        void AddResult(string message) => results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = message });

                        if (rule.NumericRules.GreaterThan.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.GreaterThan.Value);
                            if (ruleVal is IComparable ruleComp && comp.CompareTo(ruleComp) <= 0)
                                AddResult($"{propertyName} must be greater than {rule.NumericRules.GreaterThan}.");
                        }

                        if (rule.NumericRules.GreaterThanOrEqualTo.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.GreaterThanOrEqualTo.Value);
                            if (ruleVal is IComparable ruleComp && comp.CompareTo(ruleComp) < 0)
                                AddResult($"{propertyName} must be greater than or equal to {rule.NumericRules.GreaterThanOrEqualTo}.");
                        }

                        if (rule.NumericRules.LessThan.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.LessThan.Value);
                            if (ruleVal is IComparable ruleComp && comp.CompareTo(ruleComp) >= 0)
                                AddResult($"{propertyName} must be less than {rule.NumericRules.LessThan}.");
                        }

                        if (rule.NumericRules.LessThanOrEqualTo.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.LessThanOrEqualTo.Value);
                            if (ruleVal is IComparable ruleComp && comp.CompareTo(ruleComp) > 0)
                                AddResult($"{propertyName} must be less than or equal to {rule.NumericRules.LessThanOrEqualTo}.");
                        }

                        if (rule.NumericRules.EqualTo.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.EqualTo.Value);
                            if (!object.Equals(value, ruleVal))
                                AddResult($"{propertyName} must be equal to {rule.NumericRules.EqualTo}.");
                        }

                        if (rule.NumericRules.NotEqualTo.HasValue)
                        {
                            var ruleVal = ConvertRuleValue(rule.NumericRules.NotEqualTo.Value);
                            if (object.Equals(value, ruleVal))
                                AddResult($"{propertyName} must not be equal to {rule.NumericRules.NotEqualTo}.");
                        }

                        if (rule.NumericRules.BetweenMin.HasValue && rule.NumericRules.BetweenMax.HasValue)
                        {
                            var min = ConvertRuleValue(rule.NumericRules.BetweenMin.Value);
                            var max = ConvertRuleValue(rule.NumericRules.BetweenMax.Value);
                            if (min is IComparable minComp && max is IComparable maxComp &&
                                (comp.CompareTo(minComp) < 0 || comp.CompareTo(maxComp) > 0))
                                AddResult($"{propertyName} must be between {rule.NumericRules.BetweenMin} and {rule.NumericRules.BetweenMax}.");
                        }
                    }


                    if (rule.DateRules != null && value is DateTime dateVal)
                    {
                        if (rule.DateRules.MustBeAfter.HasValue && dateVal <= rule.DateRules.MustBeAfter.Value)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be after {rule.DateRules.MustBeAfter}." });

                        if (rule.DateRules.MustBeBefore.HasValue && dateVal >= rule.DateRules.MustBeBefore.Value)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be before {rule.DateRules.MustBeBefore}." });

                        if (rule.DateRules.BetweenStartDate.HasValue && rule.DateRules.BetweenEndDate.HasValue &&
                            (dateVal < rule.DateRules.BetweenStartDate || dateVal > rule.DateRules.BetweenEndDate))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must be between {rule.DateRules.BetweenStartDate} and {rule.DateRules.BetweenEndDate}." });
                    }

                    if (rule.CollectionRules != null && value is ICollection collection)
                    {
                        if (rule.NotEmptyCollection == true && value is ICollection col && col.Count == 0)
                            results.Add(new ValidationResults { PropertyName = path, ErrorMessage = $"{path} must not be empty." });
                        if (rule.CollectionRules.CollectionMinCount.HasValue && collection.Count < rule.CollectionRules.CollectionMinCount)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must have at least {rule.CollectionRules.CollectionMinCount} items." });

                        if (rule.CollectionRules.CollectionMaxCount.HasValue && collection.Count > rule.CollectionRules.CollectionMaxCount)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must have at most {rule.CollectionRules.CollectionMaxCount} items." });

                        if (rule.CollectionRules.CollectionExactCount.HasValue && collection.Count != rule.CollectionRules.CollectionExactCount)
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must have exactly {rule.CollectionRules.CollectionExactCount} items." });

                        if (rule.CollectionRules.NoNullItems == true && collection.Cast<object>().Any(x => x == null))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must not contain null items." });

                        if (rule.CollectionRules.NoDuplicates == true && collection.Cast<object>().GroupBy(x => x).Any(g => g.Count() > 1))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = $"{propertyName} must not contain duplicate items." });
                    }

                    if (rule.CustomRule != null && CustomValidators.Rules.TryGetValue(rule.CustomRule.RuleKey, out var customFunc))
                    {
                        if (!customFunc(value))
                            results.Add(new ValidationResults { PropertyName = propertyName, PropertyPath = path, ErrorMessage = rule.CustomRule.ErrorMessage });
                    }
                }
            }

            return (results.Count == 0, results);
        }

        private static object GetPropertyValue(object obj, string propertyPath)
        {
            var props = propertyPath.Split('.');
            foreach (var prop in props)
            {
                if (obj == null) return null;
                var type = obj.GetType();
                var property = type.GetProperty(prop);
                obj = property?.GetValue(obj, null);
            }
            return obj;
        }
        private static List<string> ExpandWildcardPaths(object obj, string path)
        {
            var paths = new List<string>();

            if (!path.Contains("[*]"))
            {
                paths.Add(path);
                return paths;
            }

            var prefix = path.Substring(0, path.IndexOf("[*]"));
            var suffix = path.Substring(path.IndexOf("[*]") + 3).TrimStart('.');

            var collection = GetPropertyValue(obj, prefix) as IEnumerable;
            if (collection == null) return paths;

            int i = 0;
            foreach (var _ in collection)
            {
                string fullPath = $"{prefix}[{i}]";
                if (!string.IsNullOrEmpty(suffix))
                    fullPath += $".{suffix}";

                paths.Add(fullPath);
                i++;
            }

            return paths;
        }
        //object ConvertRuleValue(object ruleValue)
        //{
        //    try
        //    {
        //        return Convert.ChangeType(ruleValue, valueType);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
