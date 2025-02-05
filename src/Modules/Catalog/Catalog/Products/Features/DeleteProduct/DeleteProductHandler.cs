using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id)
    : ICommand<DeleteProductResult>;

public sealed record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly CatalogDbContext _context;

    public DeleteProductHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteProductResult> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([command.Id], cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
