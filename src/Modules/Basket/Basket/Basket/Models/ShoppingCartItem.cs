using System.Text.Json.Serialization;

namespace Basket.Basket.Models;

public class ShoppingCartItem : Entity<Guid>
{
    [JsonConstructor]
    public ShoppingCartItem(
        Guid id,
        Guid shoppingCartId,
        Guid productId,
        int quantity,
        string color,
        decimal price,
        string productName)
    {
        Id = id;
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    internal ShoppingCartItem(
        Guid shoppingCartId,
        Guid productId,
        int quantity,
        string color,
        decimal price,
        string productName)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    public Guid ShoppingCartId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; set; }

    public string Color { get; private set; }

    public decimal Price { get; private set; }

    public string ProductName { get; private set; }

}
