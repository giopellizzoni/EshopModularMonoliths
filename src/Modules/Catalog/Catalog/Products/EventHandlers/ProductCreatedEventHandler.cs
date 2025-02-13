using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        ProductCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Namespace);

        var integrationEvent = new ProductCreatedIntegrationEvent();

        return Task.CompletedTask;
    }
}
