using System.Collections.Generic;
using System.Linq;
using DomainModel.Common;
using DomainModel.DomainErrors;
using XResults;

namespace DomainModel;

public class State : ValueObject
{
    public string Value { get; }

    private State(string value)
    {
        Value = value;
    }

    public static Result<State, Error> Create(string input, IEnumerable<string> allStates)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Errors.State.IsRequired();
        }

        string name = input.Trim().ToUpper();
        if (name.Length > 2)
        {
            return Errors.State.IsTooLong(name);
        }

        if (allStates.Any(x => x == name) == false)
        {
            return Errors.State.DoesNotExist(name);
        }

        return new State(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
