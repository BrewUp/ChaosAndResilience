using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.Domain.Entities;

public class DeliveryAddress : EntityBase
{
    internal AddressName AddressName;
    internal Street Street;
    internal City City;
    internal State State;
    internal ZipCode ZipCode;
    internal PhoneNumber PhoneNumber;
    
    protected DeliveryAddress()
    {}
    
    internal static DeliveryAddress CreateDeliveryAddress(AddressName addressName, Street street, City city, State state, ZipCode zipCode, PhoneNumber phoneNumber) =>
        new(addressName, street, city, state, zipCode, phoneNumber);
    
    private DeliveryAddress(AddressName addressName, Street street, City city, State state, ZipCode zipCode, PhoneNumber phoneNumber)
    {
        AddressName = addressName;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;}
}