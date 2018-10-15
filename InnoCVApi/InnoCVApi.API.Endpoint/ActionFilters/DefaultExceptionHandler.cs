using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using InnoCVApi.API.Endpoint.Contracts;
using InnoCVApi.Core.Common;
using InnoCVApi.Core.Logging;
using InnoCVApi.Core.Resources;

namespace InnoCVApi.API.Endpoint.ActionFilters
{
    /// <summary>
    /// Default exception handler class. Logs exceptions and hides exception messages to the client
    /// </summary>
    public class DefaultExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public DefaultExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is HttpResponseException)
            {
                return;
            }

            var headers = context.Request.Headers;

            var activityId
                = headers.Contains("X-Nonce")
                    ? headers.GetValues("X-Nonce").FirstOrDefault()
                    : Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);

            var targetException = context.Exception.GetInnerException();

            var isIoError = targetException.IsSocketException()
                            || targetException.IsTimeout()
                            || targetException.IsWin32Exception()
                            || targetException.IsSqlException();

            _logger.LogError(activityId, isIoError ? context.Exception : targetException);

            var defaultDiagnosis = new DiagnosisContract(HttpStatusCode.InternalServerError, CoreApiStringResources.Error_InternalServerError);
            var defaultResponse = context.Request.CreateResponse(HttpStatusCode.InternalServerError, defaultDiagnosis);

            context.Result = new ResponseMessageResult(defaultResponse);
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);

            return Task.FromResult(context.Result);
        }
    }
}