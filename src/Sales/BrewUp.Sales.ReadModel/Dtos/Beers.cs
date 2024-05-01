using System.Text.Json.Nodes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.ReadModel.Dtos;

public class Beers : EntityBase
{
    public string BeerName { get; private set; }
    
    protected Beers()
    {}
    
    public static Beers CreateBeer(BeerId beerId, BeerName beerName) => new(beerId.Value.ToString(), beerName.Value);
    
    private Beers(string beerId, string beerName)
    {
        Id = beerId;
        BeerName = beerName;
    }

    public BeerJson ToJson() => new ()
    {
        BeerId = Id,
        BeerName = BeerName
    };
}