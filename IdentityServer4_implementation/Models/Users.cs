using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace IdentityServer4_implementation.Models
{
    internal static class Users
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "A5E",
                    Username = "rustam",
                    Password = "rus_pass",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, "adminchik")
                    }
                }
            };
        }
    }
}