using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using XResults;

namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class State
    {
        public static Error IsRequired()
        {
            return new Error("state.required", "State is required");
        }

        public static Error IsTooLong(string state)
        {
            var details = new Dictionary<string, object?>() { ["state"] = state };
            return new Error("state.too.long", "State is too long", details);
        }

        public static Error DoesNotExist(string state)
        {
            var details = new Dictionary<string, object?>() { ["state"] = state };
            return new Error("state.does.not.exist", "State does not exist", details);
        }
    }
}
