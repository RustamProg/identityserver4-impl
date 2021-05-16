using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServer4_implementation.Models;

namespace IdentityServer4_implementation.Services
{
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userRepository.FindAsync(context.UserName);
                if (user != null)
                {
                    if (user.Password == context.Password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user)
                            );
                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User doesn't exist");
                return;
            }
            catch (Exception ex)
            {
                context.Result =
                    new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        public static Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, (user.FirstName ?? "Anon") + " " + (user.LastName ?? "Anonymous")),
                new Claim(JwtClaimTypes.GivenName, user.FirstName ?? "Anon"),
                new Claim(JwtClaimTypes.FamilyName, user.LastName ?? "Anonymous"),
                new Claim(JwtClaimTypes.NickName, user.Username ?? "No one")
            };
        }
    }
}