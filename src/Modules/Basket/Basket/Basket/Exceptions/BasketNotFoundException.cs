using Shared.Exceptions;

namespace Basket.Basket.Exceptions;

public class BasketNotFoundException : NotFoundException
{

    public BasketNotFoundException(string userName) : base($"Basket for user: {userName} was not found.")
    {

    }

}
