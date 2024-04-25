using DomainModel.Common;
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

    public static Result<Address> Create(string street, string city, State state, string zipCode)
    {
        street = (street ?? "").Trim();
        city = (city ?? "").Trim();
        zipCode = (zipCode ?? "").Trim();

        if (street.Length < 1 || street.Length > 100)
        {
            return Result.Fail("Invalid street length");
        }

        if (city.Length < 1 || city.Length > 40)
        {
            return Result.Fail("Invalid city length");
        }

        if (zipCode.Length < 1 || zipCode.Length > 5)
        {
            return Result.Fail("Invalid zip code length");
        }

        return new Address(street, city, state, zipCode);
    }
}
