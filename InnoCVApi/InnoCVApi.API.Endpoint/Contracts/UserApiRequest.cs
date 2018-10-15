using System;
using System.ComponentModel.DataAnnotations;
using InnoCVApi.Core.Resources;
using Newtonsoft.Json;

namespace InnoCVApi.API.Endpoint.Contracts
{
    /// <summary>
    /// Data contract for User 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserApiRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("id", Order = 1)]
        [Required(ErrorMessageResourceName = "Message_RequiredId", ErrorMessageResourceType = typeof(ValidationStringResources))]
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [JsonProperty("name", Order = 2)]
        [Required(ErrorMessageResourceName = "Message_RequiredName",
            ErrorMessageResourceType = typeof(ValidationStringResources))]
        public string Name { get; set; }

        /// <summary>
        /// User birthdate
        /// </summary>
        [JsonProperty("birthdate", Order = 3)]
        [Required(ErrorMessageResourceName = "Message_RequiredBirthDate",
            ErrorMessageResourceType = typeof(ValidationStringResources))]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d\s(0?[0-9]|1[0-9]?|2[0-3]):(0[0-9]|[1-5][0-9]):(0[0-9]|[1-5][0-9])$",
            ErrorMessageResourceName = "Message_InvalidBirthDate",
            ErrorMessageResourceType = typeof(ValidationStringResources), MatchTimeoutInMilliseconds = 5000)]
        public DateTime BirthDate { get; set; }
    }
}