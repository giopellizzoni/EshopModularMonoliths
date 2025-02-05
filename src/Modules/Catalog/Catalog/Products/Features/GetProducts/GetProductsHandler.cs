using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetProductsResult>;

public sealed record GetProductsResult(PaginatedResult<ProductDto> Products);

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
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await _context.Products.LongCountAsync(cancellationToken);

        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsResult(new PaginatedResult<ProductDto>(pageIndex, pageSize, totalCount, productDtos));
    }
}
