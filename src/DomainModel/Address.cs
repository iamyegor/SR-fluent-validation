using DomainModel.Common;
using DomainModel.DomainErrors;
using XResults;

namespace DomainModel;

public class Address : Entity
{
    public string Street { get; }
    public string City { get; }
    public State State { get; }
    public string ZipCode { get; }

    private Address(string street, string city, State state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public static Result<Address, Error> Create(
        string? street,
        string? city,
        State state,
        string? zipCode
    )
    {
        street = (street ?? "").Trim();
        city = (city ?? "").Trim();
        zipCode = (zipCode ?? "").Trim();

        if (street.Length < 1 || street.Length > 100)
        {
            return Errors.Address.HasInvalidStreetLength(street);
        }

        if (city.Length < 1 || city.Length > 40)
        {
            return Errors.Address.HasInvalidCityLength(city);
        }

        if (zipCode.Length < 1 || zipCode.Length > 5)
        {
            return Errors.Address.HasInvalidZipCodeLength(zipCode);
        }

        return new Address(street, city, state, zipCode);
    }
}
