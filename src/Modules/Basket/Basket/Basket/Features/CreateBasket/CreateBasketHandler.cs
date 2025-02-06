namespace Basket.Basket.Features.CreateBasket;

public sealed record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;

public sealed record CreateBasketResult(Guid Id);

public class CreateBasketHandler : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    private readonly BasketDbContext _context;

    public CreateBasketHandler(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<CreateBasketResult> Handle(
        CreateBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = CreateShoppingCart(command.ShoppingCart);

        _context.ShoppingCarts.Add(shoppingCart);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private static ShoppingCart CreateShoppingCart(ShoppingCartDto shoppingCartDto)
    {
        var newShoppingCart = ShoppingCart.Create(Guid.NewGuid(), shoppingCartDto.UserName);

        shoppingCartDto.Items.ForEach(
            item =>
            {
                var cartItem = item.Adapt<ShoppingCartItem>();
                newShoppingCart.AddItem(cartItem);
            });

        return newShoppingCart;
    }
}
