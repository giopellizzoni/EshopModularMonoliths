namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(bool IsSuccess);

public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly CatalogDbContext _context;

    public UpdateProductHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FindAsync([command.Product.Id], cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product with id {command.Product.Id} not found.");
        }

        product.Update(
            command.Product.Name,
            command.Product.Categories,
            command.Product.Description,
            command.Product.ImageFile,
            command.Product.Price);

        return new UpdateProductResult(true);
    }
}
