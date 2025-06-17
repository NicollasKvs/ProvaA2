using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using api_avaliacao.Models;
using api_avaliacao.Models;
using api_avaliacao.Data;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDataContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario login)
    {
        var user = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Senha, user.Senha))
            return Unauthorized();

        var token = GerarToken(user);
        return Ok(new { token });
    }

    private string GerarToken(Usuario user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
