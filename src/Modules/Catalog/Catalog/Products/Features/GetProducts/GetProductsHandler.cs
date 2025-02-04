namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsQuery()
    : IQuery<GetProductsResult>;

public sealed record GetProductsResult(IEnumerable<ProductDto> Products);

public class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly CatalogDbContext _context;

    public GetProductsHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductsResult> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = MapToProductDtos(products);

        return new GetProductsResult(productDtos);
    }

    private static IEnumerable<ProductDto> MapToProductDtos(List<Product> products)
    {
        return products.Select(
            p => new ProductDto(
                p.Id,
                p.Name,
                p.Categories,
                p.Description,
                p.ImageFile,
                p.Price));
    }
}
