using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/prod" +
                "ucts",
                async (
                    [AsParameters] PaginationRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetProductsQuery(request);

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
