using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.ReadModel.Dtos;

public class DeliveryAddress
{
    public string Name { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    
    protected DeliveryAddress()
    {}
    
    public static DeliveryAddress CreateDeliveryAddress(AddressName name, Street street, City city, State state, ZipCode zipCode, PhoneNumber phoneNumber) =>
        new(name.Value, street.Value, city.Value, state.Value, zipCode.Value, phoneNumber.Value);

    private DeliveryAddress(string name, string street, string city, string state, string zipCode, string phoneNumber)
    {
        Name = name;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
    }
    
    public DeliveryAddressJson ToJson() => new(Name, Street, City, State, ZipCode, PhoneNumber);
}