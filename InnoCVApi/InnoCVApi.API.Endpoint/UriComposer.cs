using System;
using System.Web;

namespace InnoCVApi.API.Endpoint
{
    /// <summary>
    /// Utility class for uri composition
    /// </summary>
    public class UriComposer
    {
        /// <summary>
        /// Compose an Uri for resource location
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="resourceKey"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static Uri ComposeLocationUri<TKey>(string baseAddress, string route, TKey resourceKey)
        {
            var uri = new Uri($"{baseAddress}/api/{route}/{resourceKey}");
            return uri;
        }
    }
}