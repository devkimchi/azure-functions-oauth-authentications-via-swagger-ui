using System.Collections.Generic;
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
    public static class OAuthEasyAuthFlowHttpTrigger
    {
        [FunctionName(nameof(OAuthEasyAuthFlowHttpTrigger))]
        [OpenApiOperation(operationId: "oauth.flows.easyauth", tags: new[] { "oauth" }, Summary = "OAuth easy auth flows - MUST be deployed to Azure", Description = "This shows the OAuth easy auth flows. To use this feature, this function app MUST be deployed to Azure.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<string>), Summary = "successful operation", Description = "successful operation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var claims = req.HttpContext.User.Claims.Select(p => p.ToString());

            var result = new OkObjectResult(claims);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}