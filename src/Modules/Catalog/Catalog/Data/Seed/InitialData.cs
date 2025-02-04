namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create("IPhone X", ["category1"], "Apple IPhone X ", "imagefile", 1000),
        Product.Create("IPhone 11", ["category1"], "Apple IPhone 11", "imagefile", 1200),
        Product.Create("Samsung Galaxy S21", ["category2"], "Samsung Galaxy S21", "imagefile", 800),
        Product.Create("Google Pixel 5", ["category3"], "Google Pixel 5", "imagefile", 700),
        Product.Create("OnePlus 9", ["category4"], "OnePlus 9", "imagefile", 750),
    };
}
