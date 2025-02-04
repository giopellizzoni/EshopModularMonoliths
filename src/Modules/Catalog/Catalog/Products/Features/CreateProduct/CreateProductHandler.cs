namespace Catalog.Products.Features.CreateProduct;

public sealed record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly CatalogDbContext _context;

    public CreateProductHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private static Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            productDto.Name,
            productDto.Categories,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);

        return product;
    }
}
