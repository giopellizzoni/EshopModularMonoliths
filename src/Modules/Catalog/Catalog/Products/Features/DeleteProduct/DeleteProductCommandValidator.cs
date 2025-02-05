namespace Catalog.Products.Features.DeleteProduct;

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }

}
