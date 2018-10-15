using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using InnoCVApi.API.Endpoint.Contracts;
using InnoCVApi.Core.Resources;

namespace InnoCVApi.API.Endpoint.ActionFilters
{
    /// <summary>
    /// Validation attribute for request's body
    /// </summary>
    public class ValidateBodyAttribute : ActionFilterAttribute
    {
        private readonly string _argumentKey;

        public override bool AllowMultiple => false;

        public ValidateBodyAttribute(string argumentKey)
        {
            _argumentKey = argumentKey;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            DiagnosisContract diagnosis;

            if (!ArgumentKeyExists(actionContext, _argumentKey))
            {
                diagnosis = new DiagnosisContract(HttpStatusCode.BadRequest,
                    ValidationStringResources.Error_InvalidBody);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, diagnosis);

                return;
            }

            var body = actionContext.ActionArguments[_argumentKey];

            if (body == null)
            {
                diagnosis = new DiagnosisContract(HttpStatusCode.BadRequest,
                    ValidationStringResources.Error_InvalidBody);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, diagnosis);

                return;
            }

            if (actionContext.ModelState.IsValid) return;

            var lstModelErrors = actionContext.ModelState.Values.SelectMany(e => e.Errors).ToList();
            var firstModelError = lstModelErrors.FirstOrDefault(error => !string.IsNullOrEmpty(error.ErrorMessage));
            var diagnosisMessage = firstModelError == null
                ? ValidationStringResources.Error_InvalidBody
                : firstModelError.ErrorMessage;

            diagnosis = new DiagnosisContract(HttpStatusCode.BadRequest, diagnosisMessage);
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, diagnosis);
        }

        private static bool ArgumentKeyExists(HttpActionContext actionContext, string argumentKey)
        {
            return actionContext.ActionArguments.ContainsKey(argumentKey);
        }

    }
}