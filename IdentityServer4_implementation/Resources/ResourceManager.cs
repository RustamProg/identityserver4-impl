using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.OpenApi.Writers;

namespace IdentityServer4_implementation.Resources
{
    internal static class ResourceManager
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "app.api.something",
                    DisplayName = "Some apis",
                    ApiSecrets = {new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256())},
                    Scopes = new List<string>{
                        "app.api.something.read",
                        "app.api.something.write",
                        "app.api.something.full",
                    }
                },
                new ApiResource("app.api.tasks", "weather apis")
            };
    }
}