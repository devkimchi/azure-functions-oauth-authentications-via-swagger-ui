using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;

namespace FunctionApp.SecurityFlows
{
    public class ImplicitAuthFlow : OpenApiOAuthSecurityFlows
    {
        private const string AuthorisationUrl = "https://login.microsoftonline.com/{0}/oauth2/v2.0/authorize";
        private const string RefreshUrl = "https://login.microsoftonline.com/{0}/oauth2/v2.0/token";

        public ImplicitAuthFlow()
        {
            var tenantId = Environment.GetEnvironmentVariable("OpenApi__Auth__TenantId");

            this.Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri(string.Format(AuthorisationUrl, tenantId)),
                RefreshUrl = new Uri(string.Format(RefreshUrl, tenantId)),

                Scopes = { { "https://graph.microsoft.com/.default", "Default scope defined in the app" } }
            };
        }
    }
}
