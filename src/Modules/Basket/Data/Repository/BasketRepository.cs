namespace Basket.Data.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly BasketDbContext _context;

    public BasketRepository(BasketDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCart> GetBasketAsync(
        string userName,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.ShoppingCarts
            .Include(x => x.Items)
            .Where(x => x.UserName == userName);

        if (asNoTracking)
        {
            query.AsNoTracking();
        }

        var basket = await query.SingleOrDefaultAsync(cancellationToken);

        return basket ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> CreateBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default)
    {
        await _context.ShoppingCarts.AddAsync(basket, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        var cart = await GetBasketAsync(userName, false, cancellationToken);
        if (cart is null)
        {
            throw new BasketNotFoundException(userName);
        }

        _context.ShoppingCarts.Remove(cart);

        return true;
    }

    public async Task<int> SaveChangesAsync(
        string? userName = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
