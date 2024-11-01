using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GameReviews.Infrastructure.Authentication;

internal class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateToken(JwtTokenGenerateRequestDto jwtTokenGenerateRequest)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwtTokenGenerateRequest.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, jwtTokenGenerateRequest.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, jwtTokenGenerateRequest.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow + _options.Lifetime,
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public UserId GetUserIdFromToken(string token)
    {
        var claimsPrincipal = GetPrincipalFromToken(token, _options.Secret);
        var id = claimsPrincipal == null
            ? throw new Exception("invalid token exception")
            : int.Parse(claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        return new(id);
    }
    public ClaimsPrincipal? GetPrincipalFromToken(string token, string secret)
    {
        try
        {
            return _jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            }, out _);
        }
        catch
        {
            return null;
        }
    }
}

