namespace Basket.Basket.Features.DeleteBasket;

public sealed record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/basket/{userName}",
            async (
                string userName,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new DeleteBasketCommand(userName),
                    cancellationToken);

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes a basket")
            .WithDescription("Deletes a basket for a given user name.");
    }
}
