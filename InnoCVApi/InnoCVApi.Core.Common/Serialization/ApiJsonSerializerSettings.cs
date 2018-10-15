using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InnoCVApi.Core.Common.Serialization
{
    public class ApiJsonSerializerSettings : JsonSerializerSettings
    {
        public ApiJsonSerializerSettings()
        {
            Formatting = Formatting.None;
            TypeNameHandling = TypeNameHandling.None;
            NullValueHandling = NullValueHandling.Ignore;
            DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            DateFormatHandling = DateFormatHandling.IsoDateFormat;
            MissingMemberHandling = MissingMemberHandling.Ignore;

            Converters.Add(new StringEnumConverter());

            var isoDateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"
            };

            Converters.Add(isoDateTimeConverter);
        }
    }
}