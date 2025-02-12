using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Data.JsonConverters;

public class ShoppingCartConverter : JsonConverter<ShoppingCart>
{
    public override ShoppingCart? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDocument.RootElement;

        var id = rootElement.GetProperty("Id").GetGuid();
        var userName = rootElement.GetProperty("UserName").GetString();
        var items = rootElement.GetProperty("Items")
            .EnumerateArray()
            .Select(x => JsonSerializer.Deserialize<ShoppingCartItem>(x.GetRawText(), options))
            .ToList();

        var shoppingCart = ShoppingCart.Create(id, userName ?? string.Empty);

        foreach (var item in items.OfType<ShoppingCartItem>())
        {
            shoppingCart.AddItem(item);
        }

        return shoppingCart;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ShoppingCart value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("userName", value.UserName);

        writer.WritePropertyName("items");
        JsonSerializer.Serialize(writer, value.Items, options);

        writer.WriteEndObject();
    }
}
