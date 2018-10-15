using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using InnoCVApi.Core.Common.ModelMapper;

namespace InnoCVApi.API.Endpoint.Controllers
{
    /// <inheritdoc />
    public class BaseController : ApiController
    {
        
        /// <summary>
        /// Helper class to map models
        /// </summary>
        protected IModelMapper ModelMapper { get; }
        /// <summary>
        /// Unique identification for each request
        /// </summary>
        public string ActivityId => GetHeader(ApiHeaders.Nonce);
        
        /// <summary>
        /// Host base address
        /// </summary>
        protected string HostBaseAddress { get; }

        /// <summary>
        /// Returns the specified request header
        /// </summary>
        /// <param name="header">Header name</param>
        /// <returns>Request header value, otherwise null</returns>
        private string GetHeader(string header)
        {
            return Request.Headers.Contains(header) ? Request.Headers.GetValues(header).FirstOrDefault() : null;

        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="modelMapper"></param>
        public BaseController(IModelMapper modelMapper)
        {
            ModelMapper = modelMapper;
            HostBaseAddress = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}