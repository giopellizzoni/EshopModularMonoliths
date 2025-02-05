namespace Catalog.Products.Features.GetProductsByCategory;

public sealed record GetProductByCategoryQuery(string Category)
    : IQuery<GetProductByCategoryResult>;

public sealed record GetProductByCategoryResult(IEnumerable<ProductDto> Products);

public class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    private readonly CatalogDbContext _context;

    public GetProductByCategoryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByCategoryResult> Handle(
        GetProductByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.Categories.Contains(query.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productsDto = products.Adapt<List<ProductDto>>();

        return new GetProductByCategoryResult(productsDto);
    }
}
