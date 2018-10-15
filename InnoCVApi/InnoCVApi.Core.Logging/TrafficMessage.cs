using System.Collections.Generic;

namespace InnoCVApi.Core.Logging
{
    public class TrafficMessage
    {
        public string ActivityId { get; set; }
        public string TimestampUtc { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUri { get; set; }
        public string RequestContentType { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; }
        public string RequestText { get; set; }
        public bool HasRequestText => !string.IsNullOrEmpty(RequestText);
        public int ResponseStatusCode { get; set; }
        public bool HasResponseText => !string.IsNullOrEmpty(ResponseText);
        public Dictionary<string, string> ResponseHeaders { get; set; }
        public string ResponseText { get; set; }
        public string ResponseContentType { get; set; }
        public double ResponseTime { get; set; }
    }
}