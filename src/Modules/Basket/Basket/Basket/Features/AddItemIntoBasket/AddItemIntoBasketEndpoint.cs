using Microsoft.AspNetCore.Mvc;

namespace Basket.Basket.Features.AddItemIntoBasket;

public sealed record AddItemIntoBasketRequest(ShoppingCartItemDto ShoppingCartItem);

public sealed record AddItemIntoBasketResponse(Guid Id);

public class AddItemIntoBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket/{userName}/items",
                async (
                    [FromRoute] string userName,
                    [FromBody] AddItemIntoBasketRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new AddItemIntoBasketCommand(userName, request.ShoppingCartItem);
                    var result = await sender.Send(command, cancellationToken);
                    var response = result.Adapt<AddItemIntoBasketResponse>();

                    return Results.Created($"/basket/{userName}/items/{response.Id}", response);
                })
            .WithName("AddItemIntoBasket")
            .Produces<AddItemIntoBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Adds an item into the basket.")
            .WithDescription("Adds an item into the basket.");
    }
}
