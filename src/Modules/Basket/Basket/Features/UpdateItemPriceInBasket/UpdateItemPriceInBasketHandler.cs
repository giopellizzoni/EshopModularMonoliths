namespace Basket.Basket.Features.UpdateItemPriceInBasket;

public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price) : ICommand<UpdateItemPriceInBasketResult>;

public record UpdateItemPriceInBasketResult(bool IsSuccess);

public class UpdateItemPriceInBasketHandler : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    private readonly BasketDbContext _context;

    public UpdateItemPriceInBasketHandler(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateItemPriceInBasketResult> Handle(
        UpdateItemPriceInBasketCommand command,
        CancellationToken cancellationToken)
    {
        var items = await _context.ShoppingCartItems
            .Where(x => x.ProductId == command.ProductId)
            .ToListAsync(cancellationToken);

        if (items.Count == 0)
        {
            return new UpdateItemPriceInBasketResult(false);
        }

        foreach (var item in items)
        {
            item.UpdatePrice(command.Price);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketResult(true);
    }
}
