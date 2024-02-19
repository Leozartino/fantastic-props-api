using Core.Models;

namespace Core.Interfaces
{
    public interface IJwtSettingsHelper
    {
        string GenerateJWT(JwtSettings settings);
    }
}
