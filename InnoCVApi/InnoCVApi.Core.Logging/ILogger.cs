using System;

namespace InnoCVApi.Core.Logging
{
    public interface ILogger 
    {
        void LogError(string activityId, Exception exception);
    }
}