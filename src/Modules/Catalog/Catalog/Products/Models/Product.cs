namespace Catalog.Products.Models;

public class Product : Entity<Guid>
{
    public string Name { get; private set; } = default!;

    public List<string> Categories { get; private set; } = [];

    public string Description { get; private set; } = default!;

    public string ImageFile { get; private set; } = default!;

    public decimal Price { get; private set; }

    private Product() {}

    public static Product Create(
        string name,
        List<string> categories,
        string description,
        string imageFile,
        decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product
        {
            Name = name,
            Categories = categories,
            Description = description,
            Price = price,
            ImageFile = imageFile,
        };

        return product;
    }

    public void Update(
        string name,
        List<string> categories,
        string description,
        string imageFile,
        decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        Name = name;
        Categories = categories;
        Description = description;
        Price = price;
        ImageFile = imageFile;
    }
}
