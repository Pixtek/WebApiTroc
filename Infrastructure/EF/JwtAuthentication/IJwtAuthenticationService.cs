using System.Security.Claims;
using Domain;

namespace Infrastructure.EF.JwtAuthentication;

public interface IJwtAuthenticationService
{
    Users Authenticate(string email, string password);

    string GenerateToken(string secret, List<Claim> claims);
}