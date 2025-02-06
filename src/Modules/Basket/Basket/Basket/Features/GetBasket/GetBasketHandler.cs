namespace Basket.Basket.Features.GetBasket;

public sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public sealed record GetBasketResult(ShoppingCartDto ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    private readonly BasketDbContext _context;

    public GetBasketHandler(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<GetBasketResult> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken)
    {
        var shoppingCart = await _context.ShoppingCarts
            .AsNoTracking()
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.UserName == query.UserName, cancellationToken);

        if (shoppingCart is null)
        {
            throw new BasketNotFoundException(query.UserName);
        }

        var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();

        return new GetBasketResult(shoppingCartDto);
    }
}
