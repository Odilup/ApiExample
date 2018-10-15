using Newtonsoft.Json;

namespace InnoCVApi.Core.Common.Serialization
{
    public class JsonNetSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonNetSerializer()
        {
            _jsonSerializerSettings = new ApiJsonSerializerSettings();
        }

        public JsonNetSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public string Serialize<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(entity, this._jsonSerializerSettings);
        }

        public TEntity Deserialize<TEntity>(string serializedContent) where TEntity : class
        {
            if (string.IsNullOrEmpty(serializedContent))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TEntity>(serializedContent);
        }

        public string Serialize(object obj)
        {
            return Serialize<object>(obj);
        }

        public object Deserialize(string serializedContent)
        {
            return Deserialize<object>(serializedContent);
        }
    }
}