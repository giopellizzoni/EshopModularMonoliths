namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsResponse(IEnumerable<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async (
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetProductsQuery();

                    var result = await sender.Send(query, cancellationToken);

                    var response = new GetProductsResponse(result.Products);

                    return Results.Ok(response);
                })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .WithSummary("Gets all products.")
            .WithDescription("Gets all products.");
    }
}
