using System.Collections.Generic;

namespace DomainModel.DomainErrors;

public static partial class Errors
{
    public static class Address
    {
        public static Error HasInvalidStreetLength(string street)
        {
            var details = new Dictionary<string, object?>() { ["street"] = street };
            return new Error("street.invalid.length", "Street has invalid length", details);
        }

        public static Error HasInvalidCityLength(string city)
        {
            var details = new Dictionary<string, object?>() { ["city"] = city };
            return new Error("city.invalid.length", "City has invalid length", details);
        }

        public static Error HasInvalidZipCodeLength(string zipCode)
        {
            var details = new Dictionary<string, object?>() { ["zipCode"] = zipCode };
            return new Error("zipCode.invalid.length", "Zip code has invalid length", details);
        }
    }
}
