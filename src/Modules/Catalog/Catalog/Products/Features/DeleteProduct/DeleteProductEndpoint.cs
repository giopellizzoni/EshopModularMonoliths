namespace Catalog.Products.Features.DeleteProduct;

public sealed record DeleteProductRequest(Guid Id);

public sealed record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/products/{id}",
                async (
                    DeleteProductRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<DeleteProductCommand>();

                    var result = await sender.Send(command, cancellationToken);

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                })
            .Produces<DeleteProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product.")
            .WithDescription("Deletes a product.");
    }
}
