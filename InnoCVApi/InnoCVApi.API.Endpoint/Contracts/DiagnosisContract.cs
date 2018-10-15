using System.Net;
using Newtonsoft.Json;

namespace InnoCVApi.API.Endpoint.Contracts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DiagnosisContract
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonProperty("statusCode", Order = 1)]
        public int StatusCode { get; set; }

        [JsonProperty("message", Order = 3)]
        public string Message { get; set; }

        public DiagnosisContract()
        {

        }

        public DiagnosisContract(HttpStatusCode httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            StatusCode = (int)httpStatusCode;
            Message = message;
        }
    }
}