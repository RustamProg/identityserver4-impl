using System.Threading.Tasks;
using IdentityServer4_implementation.Entities;

namespace IdentityServer4_implementation.Services.TokenProvider
{
    public interface ITokenProvider
    {
        Task<TokenResponse> GetToken(TokenRequest request);
    }
}