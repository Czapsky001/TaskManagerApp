using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerApp.Model;

namespace TaskManagerApp.Services.TokenService;

public class TokenService : ITokenService
{
    private const int ExpirationMinutes = 120;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, roles),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
    DateTime expiration) =>
    new(
        _configuration["Authentication:ValidIssuer"],
        _configuration["Authentication:ValidAudience"],
        claims,
        expires: expiration,
        signingCredentials: credentials
    );

    private List<Claim> CreateClaims(ApplicationUser user, IEnumerable<string> roles)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Authentication:ClaimNameSub"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            if (roles.Count() > 0) claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var signingKey = _configuration["Authentication:IssuerSigningKey"];

        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(signingKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }

}