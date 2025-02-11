namespace Basket.Basket.Features.AddItemIntoBasket;

public sealed record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem) : ICommand<AddItemIntoBasketResult>;

public sealed record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketHandler : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    private readonly IBasketRepository _repository;

    public AddItemIntoBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddItemIntoBasketResult> Handle(
        AddItemIntoBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetBasketAsync(command.UserName, false, cancellationToken);

        var shoppingCartItem = command.ShoppingCartItem.Adapt<ShoppingCartItem>();

        shoppingCart?.AddItem(shoppingCartItem);

        await _repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult(shoppingCartItem.Id);
    }
}
