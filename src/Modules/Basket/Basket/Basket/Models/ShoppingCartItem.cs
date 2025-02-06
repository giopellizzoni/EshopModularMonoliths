namespace Basket.Basket.Models;

public class ShoppingCartItem : Entity<Guid>
{
    public ShoppingCartItem(
        Guid productId,
        int quantity,
        string color,
        decimal price,
        string productName)
    {
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    public Guid ProductId { get; private set; } = default!;

    public int Quantity { get; set; } = default;

    public string Color { get; private set; } = default!;

    public decimal Price { get; private set; } = default!;

    public string ProductName { get; private set; } = default!;
}
