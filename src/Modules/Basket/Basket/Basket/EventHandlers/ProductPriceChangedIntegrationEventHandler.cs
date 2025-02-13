using Basket.Basket.Features.UpdateItemPriceInBasket;

using MassTransit;

using Microsoft.Extensions.Logging;

using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler : IConsumer<ProductPriceChangedIntegrationEvent>
{
    private readonly ISender _sender;
    private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;

    public ProductPriceChangedIntegrationEventHandler(
        ISender sender,
        ILogger<ProductPriceChangedIntegrationEventHandler> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        _logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Failed to update price in basket for product {ProductId}", context.Message.ProductId);
        }
    }
}
