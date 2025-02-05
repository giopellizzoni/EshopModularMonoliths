namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler: INotificationHandler<ProductPriceChangedEvent>
{
    public readonly ILogger<ProductPriceChangedEventHandler> _Logger;

    public ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
    {
        _Logger = logger;
    }

    public Task Handle(
        ProductPriceChangedEvent notification,
        CancellationToken cancellationToken)
    {
        _Logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
