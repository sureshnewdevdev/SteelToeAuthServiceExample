using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet("public")]
    [SwaggerOperation(Summary = "Public endpoint accessible without authentication.")]
    public IActionResult PublicEndpoint()
    {
        return Ok("This is a public endpoint.");
    }

    [HttpGet("secure")]
    [Authorize]
    [SwaggerOperation(Summary = "Secure endpoint requiring authentication.")]
    public IActionResult SecureEndpoint()
    {
        return Ok("This is a secure endpoint. You are authenticated!");
    }

    [HttpGet("admin")]
    [Authorize(Policy = "AdminOnly")]
    [SwaggerOperation(Summary = "Admin-only endpoint requiring 'admin' role.")]
    public IActionResult AdminEndpoint()
    {
        return Ok("This is an admin-only endpoint. You have the right role!");
    }
}
