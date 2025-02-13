using MassTransit;

using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
{
    private readonly ILogger<ProductPriceChangedEventHandler> _logger;
    private readonly IBus _bus;

    public ProductPriceChangedEventHandler(
        ILogger<ProductPriceChangedEventHandler> logger,
        IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(
        ProductPriceChangedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

        var integrationEvent = new ProductPriceChangedIntegrationEvent
        {
            ProductId = notification.Product.Id,
            Name = notification.Product.Name,
            Categories = notification.Product.Categories,
            Description = notification.Product.Description,
            ImageFile = notification.Product.ImageFile,
            Price = notification.Product.Price
        };

        await _bus.Publish(integrationEvent, cancellationToken);
    }
}
