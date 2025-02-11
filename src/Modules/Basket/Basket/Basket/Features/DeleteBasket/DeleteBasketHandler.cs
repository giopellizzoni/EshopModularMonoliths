namespace Basket.Basket.Features.DeleteBasket;

public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public sealed record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    private readonly IBasketRepository _repository;

    public DeleteBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken)
    {
        var isDeleted = await _repository.DeleteBasketAsync(command.UserName, cancellationToken);

        return new DeleteBasketResult(isDeleted);
    }
}
