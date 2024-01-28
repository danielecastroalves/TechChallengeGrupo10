using System.ComponentModel;
using FintechGrupo10.WebApi.Swagger;
using Newtonsoft.Json;
using Xunit;

namespace FintechGrupo10.Tests.UnitTests.WebApi.Swagger
{
    public class EnumDescriptionConverterTests
    {
        private readonly EnumDescriptionConverter _converter;
        private readonly StringWriter _writer;
        private readonly JsonSerializer _serializer;

        public EnumDescriptionConverterTests()
        {
            _converter = new();
            _writer = new();
            _serializer = new();
        }

        [Fact]
        public void WriteJson_ShouldSerializeEnumToDescription_WhenConvertingEnumToDescription()
        {
            // Act
            _converter.WriteJson(new JsonTextWriter(_writer), TestValues.Value1, _serializer);

            // Assert
            var result = _writer.ToString();
            Assert.Equal("\"Value1 Description\"", result);
        }

        [Fact]
        public void WriteJson_ShouldUseEnumToString_WhenConvertingEnumWithoutDescriptionToEnumToString()
        {
            // Act
            _converter.WriteJson(new JsonTextWriter(_writer), TestValues.ValueWithoutDescription, _serializer);

            // Assert
            var result = _writer.ToString();
            Assert.Equal("\"ValueWithoutDescription\"", result);
        }

        private enum TestValues
        {
            [Description("Value1 Description")]
            Value1 = 0,

            ValueWithoutDescription = 1
        }
    }
}
