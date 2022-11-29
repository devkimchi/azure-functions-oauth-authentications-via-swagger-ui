using FunctionApp.Configurations;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]
namespace FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IOpenApiHttpTriggerAuthorization>(p =>
            {
                var accessor = p.GetService<IHttpContextAccessor>();
                var auth = new OpenApiHttpTriggerAuthorization() { HttpContextAccessor = accessor };

                return auth;
            });
        }
    }
}