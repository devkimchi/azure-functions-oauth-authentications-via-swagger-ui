using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FunctionApp.HttpTriggers
{
    public static class OpenIDConnectAuthFlowHttpTrigger
    {
        [FunctionName(nameof(OpenIDConnectAuthFlowHttpTrigger))]
        [OpenApiOperation(operationId: "openidconnect", tags: new[] { "oidc" }, Summary = "OpenID Connect auth flows", Description = "This shows the OpenID Connect auth flows", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("oidc_auth", SecuritySchemeType.OpenIdConnect, OpenIdConnectUrl = "https://login.microsoftonline.com/{tenant_id}/v2.0/.well-known/openid-configuration", OpenIdConnectScopes = "openid,profile")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<string>), Summary = "successful operation", Description = "successful operation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var headers = req.Headers.ToDictionary(p => p.Key, p => (string) p.Value);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(headers["Authorization"].Split(' ').Last());
            var claims = token.Claims.Select(p => p.ToString());
            var content = new { headers = headers, claims = claims };

            var result = new OkObjectResult(content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
