using System;
using System.Collections.Generic;
using System.IO;
using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;

namespace InnoCVApi.Core.Logging
{
    public class JsonTrafficLayout : LayoutSkeleton
    {
        private static readonly string _machineName = Environment.MachineName;

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };

        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (!(loggingEvent.MessageObject is TrafficMessage trafficMessage))
            {
                return;
            }

            dynamic requestObj = null;

            if (trafficMessage.HasRequestText && trafficMessage.RequestContentType == "application/json")
            {
                requestObj = JsonConvert.DeserializeObject(trafficMessage.RequestText, _serializerSettings);
            }

            if (trafficMessage.RequestText.Contains("secret") && requestObj?.secret != null)
            {
                requestObj.secret = "******************************";
            }

            object responseObj = null;

            if (trafficMessage.HasResponseText && trafficMessage.ResponseContentType == "application/json")
            {
                responseObj = JsonConvert.DeserializeObject(trafficMessage.ResponseText, _serializerSettings);
            }

            var dic = new Dictionary<string, object>
            {
                { "timestampUtc", trafficMessage.TimestampUtc },
                { "activityId", trafficMessage.ActivityId },
                { "machineName", _machineName },
                { "requestMethod", trafficMessage.RequestMethod },
                { "requestUri", trafficMessage.RequestUri },
                { "requestHeaders", trafficMessage.RequestHeaders },
                { "requestContentType", trafficMessage.RequestContentType },
                { "requestBody", requestObj },
                { "responseBody", responseObj },
                { "responseContentType", trafficMessage.ResponseContentType },
                { "responseHeaders", trafficMessage.ResponseHeaders },
                { "responseTime", trafficMessage.ResponseTime },
                { "responseStatusCode", trafficMessage.ResponseStatusCode }
            };

            var json = JsonConvert.SerializeObject(dic, _serializerSettings);

            writer.Write(json);
        }
    }
}
