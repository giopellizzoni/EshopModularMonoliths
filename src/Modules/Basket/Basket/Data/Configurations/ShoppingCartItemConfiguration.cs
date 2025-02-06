namespace Basket.Data.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ProductId).IsRequired();
        builder.Property(i => i.Quantity).IsRequired();
        builder.Property(i => i.Color).IsRequired();
        builder.Property(i => i.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(i => i.ProductName).IsRequired();
    }
}
