using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Tokens;

namespace GameReviews.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public TimeSpan Lifetime { get; set; } = TimeSpan.FromHours(1);
}

