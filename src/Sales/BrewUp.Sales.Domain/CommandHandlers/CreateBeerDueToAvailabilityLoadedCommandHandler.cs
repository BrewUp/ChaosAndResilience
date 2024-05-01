using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain.CommandHandlers;

public sealed class CreateBeerDueToAvailabilityLoadedCommandHandler : CommandHandlerBaseAsync<CreateBeerDueToAvailabilityLoaded>
{
    public CreateBeerDueToAvailabilityLoadedCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(CreateBeerDueToAvailabilityLoaded command, CancellationToken cancellationToken = default)
    {
        var aggregate = Beers.CreateBeers(command.BeerId, command.BeerName, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}