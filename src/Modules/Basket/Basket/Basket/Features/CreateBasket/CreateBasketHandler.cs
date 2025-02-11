namespace Basket.Basket.Features.CreateBasket;

public sealed record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;

public sealed record CreateBasketResult(Guid Id);

public class CreateBasketHandler : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    private readonly IBasketRepository _repository;

    public CreateBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBasketResult> Handle(
        CreateBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = CreateShoppingCart(command.ShoppingCart);
        await _repository.CreateBasketAsync(shoppingCart, cancellationToken);
        return new CreateBasketResult(shoppingCart.Id);
    }

    private static ShoppingCart CreateShoppingCart(ShoppingCartDto shoppingCartDto)
    {
        var newShoppingCart = ShoppingCart.Create(Guid.NewGuid(), shoppingCartDto.UserName);

        shoppingCartDto.Items.ForEach(
            item =>
            {
                var cartItem = new ShoppingCartItem(
                    newShoppingCart.Id,
                    item.ProductId,
                    item.Quantity,
                    item.Color,
                    item.Price,
                    item.ProductName);

                newShoppingCart.AddItem(cartItem);
            });

        return newShoppingCart;
    }
}
