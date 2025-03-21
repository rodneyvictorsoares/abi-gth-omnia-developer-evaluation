using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.WebApi
{
    /// <summary>
    /// Operation filter that applies the JWT security scheme to all endpoints except those under "api/Auth".
    /// </summary>
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var relativePath = context.ApiDescription.RelativePath;

            if (!string.IsNullOrEmpty(relativePath) &&
                relativePath.StartsWith("api/Auth", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (operation.Security == null)
            {
                operation.Security = new System.Collections.Generic.List<OpenApiSecurityRequirement>();
            }

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            };

            operation.Security.Add(securityRequirement);
        }
    }
}
