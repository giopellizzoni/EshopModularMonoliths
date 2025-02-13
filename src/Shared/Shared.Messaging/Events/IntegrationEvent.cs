namespace Shared.Messaging.Events;

public record IntegrationEvent
{
    public Guid EventID => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventName => GetType().AssemblyQualifiedName;
}
