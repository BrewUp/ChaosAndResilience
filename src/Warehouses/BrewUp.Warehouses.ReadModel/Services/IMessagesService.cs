namespace BrewUp.Warehouses.ReadModel.Services;

public interface IMessagesService
{
    Task<bool> IsMessageProcessedAsync(Guid messageId, string eventType, int version, DateTime receivedOn,
        CancellationToken cancellationToken = default!);
}