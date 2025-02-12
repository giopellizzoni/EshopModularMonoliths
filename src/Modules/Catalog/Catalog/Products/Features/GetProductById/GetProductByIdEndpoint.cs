using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById;

public sealed record GetProductByIdResponse(ProductDto Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/{id}",
                async (
                    Guid Id,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetProductByIdQuery(Id);

                    var result = await sender.Send(query, cancellationToken);

                    var response = new GetProductByIdResponse(result.Product);

                    return Results.Ok(response);
                })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets a product by its ID.")
            .WithDescription("Gets a product by its ID.");
    }
}
