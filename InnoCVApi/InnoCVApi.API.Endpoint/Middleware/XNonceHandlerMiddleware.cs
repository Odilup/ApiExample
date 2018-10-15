using System;
using System.Threading.Tasks;
using InnoCVApi.Core.Common;
using Microsoft.Owin;

namespace InnoCVApi.API.Endpoint.Middleware
{

    /// <summary>
    /// Middleware to add a X-Nonce header to the request which identifies each one.
    /// </summary>
    public class XNonceHandlerMiddleware : OwinMiddleware
    {
        public XNonceHandlerMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var absolutePath = context.Request.Uri.AbsolutePath;
            if (absolutePath.IndexOf("/api", StringComparison.InvariantCultureIgnoreCase) == -1)
            {
                await Next.Invoke(context).ConfigureAwait(false);
                return;
            }
            var nonce = Nonce.NewNonce();

            context.Request.Headers["X-Nonce"] = nonce;

            await Next.Invoke(context).ConfigureAwait(false);

            context.Response.Headers["X-Nonce"] = nonce;
        }
    }
}