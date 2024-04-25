using System.Collections.Generic;
using System.Linq;
using DomainModel.Common;
using XResults;

namespace DomainModel;

public class State : ValueObject
{
    public string Value { get; }

    private State(string value)
    {
        Value = value;
    }

    public static Result<State> Create(string input, IEnumerable<string> allStates)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Result.Fail("State is required");
        }

        string name = input.Trim().ToUpper();
        if (name.Length > 2)
        {
            return Result.Fail("State is more than 2 characters");
        }

        if (allStates.Any(x => x == name) == false)
        {
            return Result.Fail("State is not an existing U.S. state");
        }

        return new State(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
