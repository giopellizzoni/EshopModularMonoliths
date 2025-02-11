namespace Basket.Data.Repository;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName, bool asNoTracking = false, CancellationToken cancellationToken = default);
    Task<ShoppingCart> CreateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
