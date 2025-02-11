namespace Basket.Basket.Features.GetBasket;

public sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public sealed record GetBasketResult(ShoppingCartDto ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    private readonly IBasketRepository _repository;

    public GetBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetBasketResult> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetBasketAsync(query.UserName, true, cancellationToken: cancellationToken);

        var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();

        return new GetBasketResult(shoppingCartDto);
    }
}
