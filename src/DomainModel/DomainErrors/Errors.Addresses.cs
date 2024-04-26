using System.Collections.Generic;

namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class Addresses
    {
        public static Error InvalidLength(int length)
        {
            var details = new Dictionary<string, object?>() { ["addressesLength"] = length };
            return new Error("address.invalid.length", "Address has invalid length", details);
        }
    }
}
