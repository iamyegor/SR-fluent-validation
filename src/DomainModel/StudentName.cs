using System.Collections.Generic;
using DomainModel.Common;
using XResults;

namespace DomainModel
{
    public class StudentName : ValueObject
    {
        public string Value { get; }

        private StudentName(string value)
        {
            Value = value;
        }

        public static Result<StudentName> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Result.Fail("Value is required");
            }

            string name = input.Trim();
            if (name.Length > 200)
            {
                return Result.Fail("Value is too long");
            }

            return new StudentName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
