using System.Collections.Generic;
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
    public static class OAuthAuthCodeAuthFlowHttpTrigger
    {
        [FunctionName(nameof(OAuthAuthCodeAuthFlowHttpTrigger))]
        [OpenApiOperation(operationId: "oauth.flows.authcode", tags: new[] { "oauth" }, Summary = "OAuth authentication code flows", Description = "This shows the OAuth authentication code flows", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("authcode_auth", SecuritySchemeType.OAuth2, Flows = typeof(AuthCodeAuthFlow))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Dictionary<string, string>), Summary = "successful operation", Description = "successful operation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var headers = req.Headers.ToDictionary(p => p.Key, p => (string) p.Value);
            var result = new OkObjectResult(headers);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
