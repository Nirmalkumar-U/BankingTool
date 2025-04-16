using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;
using BankingTool.Model;

namespace BankingTool.Api.Validators
{
    public class CommonValidator<T>
    {
        public List<Errors> Validate(string propertyName, string req)
        {
            List<Errors> errors = new List<Errors>();
            if (req == null || string.IsNullOrWhiteSpace(req.Trim()))
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is required." });
            }
            if (propertyName.ToLower().Contains("mail") && req is string email && !IsValidEmail(email))
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is not a valid email address." });
            }
            return errors;
        }
        public List<Errors> Validate(string propertyName, int? req)
        {
            List<Errors> errors = new List<Errors>();
            if (req == null || req == 0)
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is required." });
            }
            return errors;
        }
        public List<Errors> Validate(string propertyName, T[]? req)
        {
            List<Errors> errors = new List<Errors>();
            if (req == null || req.Length == 0)
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is required." });
            }
            return errors;
        }
        public List<Errors> Validate(string propertyName, T? req, List<Rules> rules)
        {
            List<Errors> errors = new List<Errors>();

            if (req == null || IsObjectEmpty(req))
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is required." });
                return errors;
            }

            foreach (var prop in req.GetType().GetProperties())
            {
                var value = prop.GetValue(req);
                string propName = prop.Name;

                // Null or Empty Check
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    errors.Add(new Errors { PropertyName = propName, ErrorMessage = $"{propName} is required." });
                    continue;
                }

                // Email Validation
                if (propName.ToLower().Contains("mail") && value is string email && !IsValidEmail(email))
                {
                    errors.Add(new Errors { PropertyName = propName, ErrorMessage = $"{propName} is not a valid email address." });
                }

                // Collection Validation
                if (value is ICollection collection && collection.Count == 0)
                {
                    errors.Add(new Errors { PropertyName = propName, ErrorMessage = $"{propName} cannot be an empty list." });
                }

                // Fetch rules for this property
                var propertyRules = rules.Where(rule => rule.PropertyName == propName).ToList();

                foreach (var rule in propertyRules)
                {
                    var ruleText = rule.Rule.ToLower();
                    int valueLength = value?.ToString()?.Length ?? 0;

                    if (ruleText.Contains("minimum length"))
                    {
                        int minLength = ExtractLength(ruleText, "minimum length");
                        if (valueLength < minLength)
                            errors.Add(new Errors { PropertyName = propName, ErrorMessage = $"{propName} must be at least {minLength} characters long." });
                    }

                    if (ruleText.Contains("maximum length"))
                    {
                        int maxLength = ExtractLength(ruleText, "maximum length");
                        if (valueLength > maxLength)
                            errors.Add(new Errors { PropertyName = propName, ErrorMessage = $"{propName} cannot exceed {maxLength} characters." });
                    }
                }
            }

            return errors;
        }
        public List<Errors> Validate1(string propertyName, T? req, List<Rules> rules)
        {
            List<Errors> errors = new List<Errors>();

            if (req == null || IsObjectEmpty(req))
            {
                errors.Add(new Errors { PropertyName = propertyName, ErrorMessage = $"{propertyName} is required." });
                return errors;
            }


            foreach (var rule in rules)
            {
                var ruleText = rule.Rule.ToLower();
                var ruleProperty = rule.PropertyName.ToLower();
                var property = req.GetType().GetProperties().Where(x => x.Name.ToLower() == ruleProperty.ToLower()).ToList();

                foreach (var prop in property)
                {
                    var value = prop.GetValue(req);
                    if (ruleText.Contains("required"))
                    {
                        if (value == null)
                        {
                            errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} is required." });
                        }
                    }
                    else if (ruleText.Contains("not empty"))
                    {
                        if (value is string str)
                        {
                            if (string.IsNullOrWhiteSpace(str))
                            {
                                errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} must not be empty." });
                            }
                        }
                        if (value is ICollection collection)
                        {
                            if (collection.Count == 0)
                            {
                                errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} must contain at least one item." });
                            }
                        }
                    }
                    else if (ruleText.Contains("minimum length"))
                    {
                        int valueLength = value?.ToString()?.Length ?? 0;
                        int minLength = ExtractLength(ruleText, "minimum length");
                        if (valueLength < minLength)
                            errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} must be at least {minLength} characters long." });
                    }
                    else if (ruleText.Contains("maximum length"))
                    {
                        int valueLength = value?.ToString()?.Length ?? 0;
                        int maxLength = ExtractLength(ruleText, "maximum length");
                        if (valueLength > maxLength)
                            errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} cannot exceed {maxLength} characters." });
                    }
                    else if (ruleText.Contains("mail format") && value is string email && !IsValidEmail(email))
                    {
                        errors.Add(new Errors { PropertyName = ruleProperty, ErrorMessage = $"{ruleProperty} is not a valid email address." });
                    }
                }
            }

            return errors;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private static bool IsObjectEmpty(T obj)
        {
            return obj.GetType().GetProperties().All(p => p.GetValue(obj) == null);
        }
        private static int ExtractLength(string input, string type)
        {
            Match match = Regex.Match(input, $@"{type} (\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : -1;
        }
    }
    public class Rules
    {
        public string PropertyName { get; set; }
        public string Rule { get; set; }
    }
    public class RulesForEach
    {
        public string PropertyName { get; set; }
        public string Rule { get; set; }
    }
}
