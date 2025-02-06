namespace Basket.Basket.Features.DeleteBasket;

public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public sealed record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    private readonly BasketDbContext _context;

    public DeleteBasketHandler(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _context
            .ShoppingCarts
            .SingleOrDefaultAsync(sp => sp.UserName == command.UserName, cancellationToken);

        if (shoppingCart is null)
        {
            throw new BasketNotFoundException(command.UserName);
        }

        _context.ShoppingCarts.Remove(shoppingCart);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteBasketResult(true);
    }
}
