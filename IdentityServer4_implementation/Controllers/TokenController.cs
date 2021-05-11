using System.Threading.Tasks;
using IdentityServer4_implementation.Entities;
using IdentityServer4_implementation.Services.TokenProvider;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4_implementation.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : Controller
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Post([FromForm]TokenRequest request)
        {
            var response = await _tokenProvider.GetToken(request);
 
            if (!string.IsNullOrEmpty(response.Error))
            {
                return new BadRequestObjectResult(response);
            }
 
            return response;
        }
    }
}