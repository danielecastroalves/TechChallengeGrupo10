using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FintechGrupo10.WebApi.Swagger
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// EnumDescriptionConverter
    /// </summary>
    public class EnumDescriptionConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var enumValue = (Enum)value!;
            var description = GetEnumDescription(enumValue);
            serializer.Serialize(writer, description);
        }

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute))!;

            return attribute?.Description ?? value.ToString();
        }
    }
}
