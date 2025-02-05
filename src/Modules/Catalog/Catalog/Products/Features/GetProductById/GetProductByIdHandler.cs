namespace Catalog.Products.Features.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(ProductDto Product);

public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly CatalogDbContext _context;

    public GetProductByIdHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        var productDto = product.Adapt<ProductDto>();

        return new GetProductByIdResult(productDto);
    }
}
