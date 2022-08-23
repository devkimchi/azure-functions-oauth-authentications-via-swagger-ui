using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;

namespace FunctionApp.SecurityFlows
{
    public class ClientCredentialsAuthFlow : OpenApiOAuthSecurityFlows
    {
        private const string TokenUrl = "https://login.microsoftonline.com/{0}/oauth2/v2.0/token";
        private const string RefreshUrl = "https://login.microsoftonline.com/{0}/oauth2/v2.0/token";

        public ClientCredentialsAuthFlow()
        {
            var tenantId = Environment.GetEnvironmentVariable("OpenApi__Auth__TenantId");

            this.ClientCredentials = new OpenApiOAuthFlow()
            {
                TokenUrl = new Uri(string.Format(TokenUrl, tenantId)),
                RefreshUrl = new Uri(string.Format(RefreshUrl, tenantId)),

                Scopes = { { "https://graph.microsoft.com/.default", "Default scope defined in the app" } }
            };
        }
    }
}