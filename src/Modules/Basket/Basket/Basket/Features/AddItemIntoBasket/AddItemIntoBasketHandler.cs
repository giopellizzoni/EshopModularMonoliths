namespace Basket.Basket.Features.AddItemIntoBasket;

public sealed record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem) : ICommand<AddItemIntoBasketResult>;

public sealed record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketHandler : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    private readonly BasketDbContext _context;

    public AddItemIntoBasketHandler(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<AddItemIntoBasketResult> Handle(
        AddItemIntoBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _context.ShoppingCarts
            .Include(x => x.Items)
            .SingleOrDefaultAsync(sp => sp.UserName == command.UserName, cancellationToken);

        if (shoppingCart is null)
        {
            throw new BasketNotFoundException(command.UserName);
        }

        var shoppingCartItem = command.ShoppingCartItem.Adapt<ShoppingCartItem>();

        shoppingCart.AddItem(shoppingCartItem);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddItemIntoBasketResult(shoppingCartItem.Id);
    }
}
