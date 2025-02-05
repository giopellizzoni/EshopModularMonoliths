namespace Catalog.Products.Features.UpdateProduct;

public sealed record UpdateProductRequest(ProductDto Product);

public sealed record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "/products",
                async (
                    UpdateProductRequest request,
                    ISender sender,
                    CancellationToken cancellantionToken) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command, cancellantionToken);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Updates a product.")
            .WithDescription("Updates a product.");
    }
}
