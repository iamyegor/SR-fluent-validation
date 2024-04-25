using System.Collections.Generic;
using System.Text.RegularExpressions;
using DomainModel.Common;
using DomainModel.DomainErrors;
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

        public static Result<Email, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Errors.Email.IsRequired();
            }

            string email = input.Trim();
            if (email.Length > 150)
            {
                return Errors.Email.IsTooLong(email);
            }

            if (Regex.IsMatch(email, @"^(.+)@(.+)$") == false)
            {
                return Errors.Email.HasInvalidSignature(email);
            }

            return new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
