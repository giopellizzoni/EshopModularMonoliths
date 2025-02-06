namespace Basket.Basket.Dtos;

public sealed record ShoppingCartItemDto(
    Guid Id,
    Guid ShoppingCartId,
    Guid ProductId,
    string Color,
    string ProductName,
    decimal Price,
    int Quantity);
