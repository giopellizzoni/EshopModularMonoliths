namespace Basket.Basket.Features.RemoveItemFromBasket;

public sealed record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;

public sealed record RemoveItemFromBasketResult(Guid Id);

public class RemoveItemFromBasketHandler : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    private readonly IBasketRepository _repository;

    public RemoveItemFromBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<RemoveItemFromBasketResult> Handle(
        RemoveItemFromBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetBasketAsync(command.UserName, cancellationToken: cancellationToken);

        shoppingCart?.RemoveItem(command.ProductId);
        await _repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new RemoveItemFromBasketResult(shoppingCart?.Id ?? Guid.Empty);
    }
}
