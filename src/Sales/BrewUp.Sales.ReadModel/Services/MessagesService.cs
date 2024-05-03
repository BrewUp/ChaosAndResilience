using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class MessagesService(ILoggerFactory loggerFactory, [FromKeyedServices("sales")] IPersister persister)
    : ServiceBase(loggerFactory, persister), IMessagesService
{
    public async Task<bool> IsMessageProcessedAsync(Guid messageId, string eventType, int version, DateTime receivedOn,
        CancellationToken cancellationToken = default!)
    {
        try
        {
            var message = await Persister.GetByIdAsync<MessagesProcessed>(messageId.ToString(), cancellationToken);
            if (message != null && !string.IsNullOrWhiteSpace(message.EventType))
                return true;
            
            message = MessagesProcessed.CreateMessagesProcessed(messageId, eventType, version, receivedOn);
            await Persister.InsertAsync(message, cancellationToken);

            return false;
        }
        catch
        {
            return true;
        }
    }
}