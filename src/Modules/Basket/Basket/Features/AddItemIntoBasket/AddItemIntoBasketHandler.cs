using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket;

public sealed record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem) : ICommand<AddItemIntoBasketResult>;

public sealed record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketHandler : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    private readonly IBasketRepository _repository;
    private readonly ISender _sender;

    public AddItemIntoBasketHandler(
        IBasketRepository repository,
        ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }

    public async Task<AddItemIntoBasketResult> Handle(
        AddItemIntoBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetBasketAsync(command.UserName, false, cancellationToken);

        var result = await _sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId), cancellationToken);
        if (result is null)
        {
            throw new ProductNotFoundException(command.ShoppingCartItem.ProductId);
        }

        shoppingCart.AddItem(
            command.ShoppingCartItem.ProductId,
            command.ShoppingCartItem.Quantity,
            command.ShoppingCartItem.Color,
            result.Product.Price,
            result.Product.Name);

        await _repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult(shoppingCart.Id);
    }
}
