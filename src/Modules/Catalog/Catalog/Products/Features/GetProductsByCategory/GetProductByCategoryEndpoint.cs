namespace Catalog.Products.Features.GetProductsByCategory;

public sealed record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/products/category/{category}",
            async (
                string category,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProductByCategoryQuery(category);

                var result = await sender.Send(query, cancellationToken);

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            });
    }
}
