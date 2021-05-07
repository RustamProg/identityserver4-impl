using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4_implementation.Controllers
{
    public class TokenController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}