namespace Basket.Basket.Features.CreateBasket;

public sealed record CreateBasketRequest(ShoppingCartDto ShoppingCart);

public sealed record CreateBasketResponse(Guid Id);

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket",
                async (
                    CreateBasketRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<CreateBasketCommand>();

                    var result = await sender.Send(command, cancellationToken);

                    var response = result.Adapt<CreateBasketResponse>();

                    return Results.Created($"/basket/{response.Id}", response);
                })
            .WithName("CreateBasket")
            .Produces<CreateBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new basket")
            .WithDescription("Creates a new basket");
    }
}
