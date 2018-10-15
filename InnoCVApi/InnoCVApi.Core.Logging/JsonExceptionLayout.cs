using System;
using System.Collections.Generic;
using System.IO;
using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;

namespace InnoCVApi.Core.Logging
{
    public class JsonExceptionLayout : LayoutSkeleton
    {
        private static readonly string _machineName = Environment.MachineName;

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
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
            if (!(loggingEvent.MessageObject is ExceptionMessage exceptionMessage))
            {
                return;
            }

            var dic = new Dictionary<string, object>
            {
                { "timestampUtc", loggingEvent.TimeStamp.ToUniversalTime().ToString("O") },
                { "activityId", exceptionMessage.ActivityId },
                { "machineName", _machineName },
                { "className", loggingEvent.LocationInformation?.ClassName },
                { "type", exceptionMessage.Type },
                { "message", exceptionMessage.Message },
                { "stackTrace", exceptionMessage.StackTrace }
            };

            var json = JsonConvert.SerializeObject(dic, _serializerSettings);

            writer.Write(json);
        }
    }
}