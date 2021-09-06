using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using FunctionApp.SecurityFlows;

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
    public static class OAuthImplicitAuthFlowHttpTrigger
    {
        [FunctionName(nameof(OAuthImplicitAuthFlowHttpTrigger))]
        [OpenApiOperation(operationId: "oauth.flows.implicit", tags: new[] { "oauth" }, Summary = "OAuth implicit flows", Description = "This shows the OAuth implicit flows", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("implicit_auth", SecuritySchemeType.OAuth2, Flows = typeof(ImplicitAuthFlow))]
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

            var result = new OkObjectResult(claims);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
