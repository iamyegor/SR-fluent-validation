using System.Collections.Generic;
using System.Text.RegularExpressions;
using DomainModel.Common;
using XResults;

namespace DomainModel
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Result.Fail("Value is required");
            }

            string email = input.Trim();
            if (email.Length > 150)
            {
                return Result.Fail("Value is too long");
            }

            if (Regex.IsMatch(email, @"^(.+)@(.+)$") == false)
            {
                return Result.Fail("Value is invalid");
            }

            return new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
