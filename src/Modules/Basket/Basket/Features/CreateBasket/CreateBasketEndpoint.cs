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
                    ClaimsPrincipal user,
                    CancellationToken cancellationToken) =>
                {
                    var userName = user.Identity!.Name;
                    if (userName is null)
                    {
                        return Results.BadRequest("User not found");
                    }

                    var updateShoppingCart = request.ShoppingCart with
                    {
                        UserName = userName,
                    };

                    var command = new CreateBasketCommand(updateShoppingCart);

                    var result = await sender.Send(command, cancellationToken);

                    var response = result.Adapt<CreateBasketResponse>();

                    return Results.Created($"/basket/{response.Id}", response);
                })
            .WithName("CreateBasket")
            .Produces<CreateBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new basket")
            .WithDescription("Creates a new basket")
            .RequireAuthorization();
    }
}
