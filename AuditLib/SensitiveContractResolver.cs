using Google.Protobuf.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Reflection;

namespace AuditLib
{
    public sealed class SensitiveContractResolver : DefaultContractResolver
    {
        private readonly string[] _sensitiveProperties;
        private const string MASK = "***";
        public SensitiveContractResolver(params string[] sensitiveProperties)
        {
            _sensitiveProperties = sensitiveProperties;
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            PropertyInfo? propertyInfo = member as PropertyInfo;
            if (propertyInfo == null)
            {
                return property;
            }
            if (Array.Exists(_sensitiveProperties, x => x.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
            {
                property.PropertyType = typeof(string);
                property.ValueProvider = new ReplaveValueProvider();
                return property;
            }
            return property;
        }
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            return new SensitiveDictionaryContract(objectType, _sensitiveProperties);
        }
        protected override JsonDynamicContract CreateDynamicContract(Type objectType)
        {
            return new SensitiveDynamicContract(objectType, _sensitiveProperties);
        }
        private sealed class SensitiveDynamicContract : JsonDynamicContract
        {
            public SensitiveDynamicContract(Type underlyingType, string[] sensitiveProperties) : base(underlyingType)
            {
                Converter = new SensitiveDictionaryConverter(sensitiveProperties);
            }
        }

        private sealed class SensitiveDictionaryContract : JsonDictionaryContract
        {
            public SensitiveDictionaryContract(Type underlyingType, string[] sensitiveProperties) : base(underlyingType)
            {
                Converter = new SensitiveDictionaryConverter(sensitiveProperties);
            }
        }
        private sealed class ReplaveValueProvider : IValueProvider
        {
            public object? GetValue(object target)
            {
                return MASK;
            }

            public void SetValue(object target, object? value)
            {
                //_MemberInfo.SetValue(target, value);
            }
        }
    }
    public sealed class SensitiveDictionaryConverter : JsonConverter
    {
        private readonly string[] _sensitiveProperties;
        private const string MASK = "***";
        public SensitiveDictionaryConverter(params string[] sensitiveProperties)
        {
            _sensitiveProperties = sensitiveProperties;
        }
        public override bool CanRead { get { return false; } }
        public override bool CanWrite { get { return true; } }
        public override void WriteJson(JsonWriter writer, object? dict, JsonSerializer serializer)
        {
            var dictionary = dict as IDictionary;
            writer.WriteStartObject();
            if (dictionary != null)
                foreach (var item in dictionary.Keys)
                {
                    var key = item.ToString();
                    writer.WritePropertyName(key);
                    if (Array.Exists(_sensitiveProperties, x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                        writer.WriteValue(MASK);
                    else
                        writer.WriteValue(dictionary[key]);
                }
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, string>) || objectType == typeof(Dictionary<string, string?>) || objectType == typeof(MapField<string, string>);
        }
    }

}
