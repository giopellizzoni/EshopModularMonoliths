namespace Basket.Basket.Features.UpdateItemPriceInBasket;

public class UpdateItemPriceInBasketCommandValidator : AbstractValidator<UpdateItemPriceInBasketCommand>
{
    public UpdateItemPriceInBasketCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}
