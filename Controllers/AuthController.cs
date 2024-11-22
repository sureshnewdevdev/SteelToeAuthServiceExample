using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("token")]
    public IActionResult GenerateToken([FromBody] LoginRequest request)
    {
        if (request.Username != "admin" || request.Password != "password")
        {
            return Unauthorized("Invalid credentials.");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim("role", "admin"),
            new Claim("type", "Regular"),
            new Claim("zone","Asia")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SSSSdsfsdfsdfsdfsdfsdfsdfsdfdsfsdfsd"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "AuthSecuredMicroservice",
            audience: "AuthSecuredMicroservice",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(3),
            signingCredentials: creds);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
