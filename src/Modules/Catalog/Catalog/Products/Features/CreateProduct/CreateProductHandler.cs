namespace Catalog.Products.Features.CreateProduct;

public sealed record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(
        CatalogDbContext context,
        ILogger<CreateProductHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateProductCommandHandler.Handle called with: {@Command}", command);

        var product = command.Product.Adapt<Product>();

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
