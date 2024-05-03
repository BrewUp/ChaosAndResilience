using BrewUp.Shared.Entities;

namespace BrewUp.Sales.ReadModel.Dtos;

public class MessagesProcessed : EntityBase
{
    public string EventType { get; private set; }
    public int AggregateVersion{ get; private set; }
    public DateTime ReceivedOn{ get; private set; }
    
    protected MessagesProcessed()
    {}

    public static MessagesProcessed CreateMessagesProcessed(Guid messageId, string eventType, int version, DateTime receivedOn) =>
        new(messageId, eventType, version, receivedOn);

    private MessagesProcessed(Guid messageId, string eventType, int version, DateTime receivedOn)
    {
        Id = messageId.ToString();
        EventType = eventType;
        AggregateVersion = version;
        ReceivedOn = receivedOn;
    }
}