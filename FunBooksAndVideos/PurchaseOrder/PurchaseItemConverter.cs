using FunBooksAndVideos.OrderItems;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace FunBooksAndVideos.PurchaseOrder
{
    public class PurchaseItemConverter : JsonConverter<IPurchaseItem>
    {
        public override IPurchaseItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDoc.RootElement;
                var typeProperty = jsonObject.GetProperty("type").GetString();

                if (typeProperty == null)
                {
                    throw new JsonException("Property 'type' is required");
                }

                return typeProperty switch
                {
                    nameof(ProductItem) => JsonSerializer.Deserialize<ProductItem>(jsonObject.GetRawText(), options) ?? throw new JsonException($"Deserialization of type {typeProperty} returned null"),
                    nameof(MembershipItem) => JsonSerializer.Deserialize<MembershipItem>(jsonObject.GetRawText(), options) ?? throw new JsonException($"Deserialization of type {typeProperty} returned null"),
                    _ => throw new NotSupportedException($"Type {typeProperty} is not supported")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, IPurchaseItem value, JsonSerializerOptions options)
        {
            var type = value.GetType().Name;
            var jsonObject = JsonSerializer.SerializeToElement(value, value.GetType(), options);
            var jsonObjectWithType = new JsonObject
            {
                ["type"] = type
            };

            foreach (var property in jsonObject.EnumerateObject())
            {
                jsonObjectWithType[property.Name] = JsonNode.Parse(property.Value.GetRawText());
            }

            jsonObjectWithType.WriteTo(writer);
        }
    }
}
