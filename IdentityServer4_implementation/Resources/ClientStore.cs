using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer4_implementation.Resources
{
    internal static class ClientStore
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "all",
                    DisplayName = "All API",
                    Description = "Just test resource",
                    Scopes = new List<string>{"all.read", "all.write"},
                    ApiSecrets = new List<Secret>{new Secret("ScopeSecretString".Sha256())}, 
                    UserClaims = new List<string>{"role"}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string>{"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Client Credential Flow",
                    ClientId = "client_credential_flow",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("client_credential_flow_secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "all.read",
                        "all.write"
                    },
                    AllowOfflineAccess = false,
                    AccessTokenLifetime = 30
                },
                
                new Client
                {
                    ClientName = "UsersLoginClient",
                    ClientId = "users_login_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("login_secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "all.read",
                        "all.write",
                        
                    },
                    AllowOfflineAccess = false,
                    AccessTokenLifetime = 30
                }
            };
        }
        
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("all.read", "Read access"),
                new ApiScope("all.write", "Write access"),
            };
        }
    }
}