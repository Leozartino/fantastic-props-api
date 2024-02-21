using Core.Models;
using System.Security.Claims;

namespace Core.Interfaces
{
    public interface IJwtSettingsHelper
    {
        string GenerateJWT(JwtSettings settings, List<Claim> claims);
    }
}
