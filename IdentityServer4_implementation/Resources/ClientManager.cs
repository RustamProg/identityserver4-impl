using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer4_implementation.Resources
{
    internal static class ClientManager
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientName = "First client",
                    ClientId = "t8agr5xKt4$3",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1919".Sha256())},
                    AllowedScopes = new List<string> {"app.api.something.read", "app.api.something.write"}
                },
                new Client
                {
                    ClientName = "Second client",
                    ClientId = "3X=nNv?Sgu$S",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256())},
                    AllowedScopes = new List<string> {"app.api.tasks"}
                },
            };
    }
}