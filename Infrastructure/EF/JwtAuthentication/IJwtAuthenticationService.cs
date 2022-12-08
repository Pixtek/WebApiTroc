using System.Security.Claims;
using Domain;
using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.JwtAuthentication;

public interface IJwtAuthenticationService
{
    DbUser? Authenticate(string email, string password);

    string GenerateToken(string secret, List<Claim> claims);
}