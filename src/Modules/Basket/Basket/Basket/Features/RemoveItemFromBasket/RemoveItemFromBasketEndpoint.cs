using Microsoft.AspNetCore.Mvc;

namespace Basket.Basket.Features.RemoveItemFromBasket;

public sealed record RemoveItemFromBasketResponse(bool IsSuccess);

public class RemoveItemFromBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/basket/{userName}/items/{productId}",
                async (
                    [FromRoute] string userName,
                    [FromRoute] Guid productId,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var command = new RemoveItemFromBasketCommand(userName, productId);
                    var result = await sender.Send(command, cancellationToken);
                    var response = result.Adapt<RemoveItemFromBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("RemoveItemFromBasket")
            .Produces<RemoveItemFromBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Removes an item from the basket.")
            .WithDescription("Removes an item from the basket.");
    }
}
