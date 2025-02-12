using System.Text.Json;
using System.Text.Json.Serialization;

using Basket.Data.JsonConverters;

using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Data.Repository;

public class CachedBasketRepository : IBasketRepository
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new ShoppingCartConverter(),
            new ShoppingCartItemConverter()
        }
    };

    private readonly IBasketRepository _repository;
    private readonly IDistributedCache _cache;

    public CachedBasketRepository(
        IBasketRepository repository,
        IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<ShoppingCart> GetBasketAsync(
        string userName,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
        {
            return await _repository.GetBasketAsync(userName, false, cancellationToken);
        }

        var cachedBasket = await _cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket, _options)!;
        }

        var basket = await _repository.GetBasketAsync(userName, asNoTracking, cancellationToken);
        await _cache.SetStringAsync(userName, JsonSerializer.Serialize(basket, _options), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> CreateBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default)
    {
        await _repository.CreateBasketAsync(basket, cancellationToken);
        await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket, _options), cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        await _repository.DeleteBasketAsync(userName, cancellationToken);
        await _cache.RemoveAsync(userName, cancellationToken);

        return true;
    }

    public async Task<int> SaveChangesAsync(
        string? userName = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.SaveChangesAsync(userName, cancellationToken);
        if (userName is not null)
        {
            await _cache.RemoveAsync(userName, cancellationToken);
        }

        return result;
    }
}
