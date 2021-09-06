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
    public static class ApiKeyInQueryAuthFlowHttpTrigger
    {
        [FunctionName(nameof(ApiKeyInQueryAuthFlowHttpTrigger))]
        [OpenApiOperation(operationId: "apikey.query", tags: new[] { "apikey" }, Summary = "API Key authentication code flow via querystring", Description = "This shows the API Key authentication code flow via querystring", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("apikeyquery_auth", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Dictionary<string, string>), Summary = "successful operation", Description = "successful operation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queries = req.Query.ToDictionary(q => q.Key, q => (string) q.Value);
            var result = new OkObjectResult(queries);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
