using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasypayBackend.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class LogoutController : ControllerBase
    {
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Invalidate token (e.g., add to blacklist - requires additional storage like Redis)
            // For demo, clear client-side token
            return Ok(new { Message = "Logged out successfully. Please clear the token client-side." });
        }
    }
}