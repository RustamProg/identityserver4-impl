using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer4_implementation.Services
{
    public class ProfileService: IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = await _userRepository.FindAsync(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        
                    }
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        var user = await _userRepository.FindAsyncById(long.Parse(userId.Value));

                        if (user != null)
                        {
                            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);
                            context.IssuedClaims =
                                claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                {
                    var user = await _userRepository.FindAsyncById(long.Parse(userId.Value));

                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            context.IsActive = user.IsActive;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}