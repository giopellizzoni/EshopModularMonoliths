using Shared.Exceptions;

namespace Basket.Basket.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : base($"Product with id: '{productId}' was not found.")
    {
    }
}
