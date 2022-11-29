using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;

namespace FunctionApp.Configurations
{
    public class OpenApiHttpTriggerAuthorization : DefaultOpenApiHttpTriggerAuthorization
    {
        public virtual IHttpContextAccessor HttpContextAccessor { get; set; }

        public override async Task<OpenApiAuthorizationResult> AuthorizeAsync(IHttpRequestDataObject req)
        {
            var result = default(OpenApiAuthorizationResult);

            var claims = this.HttpContextAccessor.HttpContext.User.Claims;
            if (!claims.Any())
            {
                result = new OpenApiAuthorizationResult()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ContentType = "text/plain",
                    Payload = "Unauthorized",
                };

                return await Task.FromResult(result).ConfigureAwait(false);
            }

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
