namespace Basket.Basket.Models;

public class ShoppingCart : Aggregate<Guid>
{
    public string UserName { get; private set; } = default!;

    private readonly List<ShoppingCartItem> _items = new();

    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public static ShoppingCart? Create(
        Guid id,
        string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);
        var shoppingCart = new ShoppingCart
        {
            Id = id,
            UserName = userName
        };

        return shoppingCart;
    }

    public void AddItem(ShoppingCartItem item)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(item.Quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(item.Price);

        var existingItem = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existingItem is not null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            _items.Add(item);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(x => x.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
        }
    }
}
