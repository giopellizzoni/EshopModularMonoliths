namespace Basket.Basket.Features.GetBasket;

public sealed record GetBasketResponse(ShoppingCartDto ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/basket/{userName}",
            async (
                string userName,
                ISender sender,
                ClaimsPrincipal user,
                CancellationToken cancellationToken) =>
            {

                var loggedUser = user.Identity!.Name;
                if(loggedUser != userName)
                {
                    return Results.Unauthorized();
                }

                var result = await sender.Send(
                    new GetBasketQuery(userName),
                    cancellationToken);

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets a basket")
            .WithDescription("Gets a basket for a given user name.")
            .RequireAuthorization();
    }
}
