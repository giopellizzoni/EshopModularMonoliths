namespace Catalog.Products.Features.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
