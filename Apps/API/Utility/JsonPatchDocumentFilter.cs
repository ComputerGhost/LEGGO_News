using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Utility
{
    /// <summary>
    /// Fixes the display of JsonPatch in Swagger
    /// </summary>
    public class JsonPatchDocumentFilter : IDocumentFilter
    {

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Remove irrelevent schemas
            var badSuffixes = new[] { "JsonPatchDocument", "Operation" };
            var badProperties = new[] { "IContractResolver", "OperationType", "ProblemDetails" };
            foreach (var schema in swaggerDoc.Components.Schemas.ToList()) {
                if (badProperties.Contains(schema.Key) || badSuffixes.Any(suffix => schema.Key.EndsWith(suffix)))
                    swaggerDoc.Components.Schemas.Remove(schema.Key);
            }

            // Add in the correct schemas
            swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "op", new OpenApiSchema{ Type = "string" } },
                    { "value", new OpenApiSchema { Type = "string" } },
                    { "path", new OpenApiSchema { Type = "string" } }
                }
            });
            swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema {
                Type = "array",
                Items = new OpenApiSchema {
                    Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
                },
                Description = "Array of operations to perform"
            });

            // Modify patch operation schemas
            var patchOperations = swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
                .Where(p => p.Key == OperationType.Patch);
            foreach (var patch in patchOperations) {

                // Remove all non-patch content-types
                var jsonRequests = patch.Value.RequestBody.Content.Where(c => c.Key != "application/json-patch+json");
                foreach (var request in jsonRequests)
                    patch.Value.RequestBody.Content.Remove(request.Key);

                // Update schema of patch request, assuming the content type is set correctly.
                var patchRequest = patch.Value.RequestBody.Content.SingleOrDefault(c => c.Key == "application/json-patch+json");
                if (patchRequest.Key == null)
                    throw new InvalidOperationException("Content type for PATCH should be application/json-patch+json");
                patchRequest.Value.Schema = new OpenApiSchema {
                    Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
                };

            }
        }

    }
}
