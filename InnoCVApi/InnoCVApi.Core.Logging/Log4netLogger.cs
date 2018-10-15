using System;
using System.Threading.Tasks;
using log4net;

namespace InnoCVApi.Core.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger()
        {
            _log = LogManager.GetLogger("exceptions");
        }

        internal Log4NetLogger(ILog log)
        {
            _log = log;
        }

        public void LogError(string activityId, Exception exception)
        {
            Task.Run(() =>
            {
                var entry = new ExceptionMessage
                {
                    ActivityId = activityId,
                    Message = exception.Message,
                    Type = exception.GetType().Name,
                    StackTrace = exception.ToString()
                };

                _log.Error(entry);
            });
        }
    }
}