namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler: INotificationHandler<ProductPriceChangedEvent>
{
    private readonly ILogger<ProductPriceChangedEventHandler> _logger;

    public ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        ProductPriceChangedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
