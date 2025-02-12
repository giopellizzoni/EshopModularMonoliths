using Shared.Contracts.CQRS;

namespace Catalog.Contracts.Products.Features.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(ProductDto Product);
