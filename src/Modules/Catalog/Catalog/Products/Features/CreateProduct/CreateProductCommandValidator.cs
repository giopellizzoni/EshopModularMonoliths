namespace Catalog.Products.Features.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Product.Categories)
            .NotEmpty()
            .WithMessage("Categories are required.");

        RuleFor(x => x.Product.ImageFile)
            .NotEmpty()
            .WithMessage("Imagefile is required.");

        RuleFor(x => x.Product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}
