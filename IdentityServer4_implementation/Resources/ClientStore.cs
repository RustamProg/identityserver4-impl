using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer4_implementation.Resources
{
    public class ClientStore
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("all", "all")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return null;
        }
    }
}