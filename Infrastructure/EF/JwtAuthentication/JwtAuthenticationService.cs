using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.EF.JwtAuthentication;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    //SIMULATION BASE DE DONNEES VITE FAIT POUR TESTER
    private readonly List<Users> _users = new List<Users>()
    {
        new Users
        {
            Id = 1,
            Email = "admin1@test.com",
            Pseudo = "admin1",
            Localite = "Mons",
            Mdp = "admin1Pwd"
        },
        new Users
        {
            Id = 2,
            Email = "user1@test.com",
            Pseudo = "user1",
            Localite = "Bassilly",
            Mdp = "user1Pwd"
        }
    };


    public Users Authenticate(string email, string password)
    {
        //ACCEDER LA BASE DE DONNEES ICI
        return _users.Where(u => u.Email.ToUpper().Equals(email.ToUpper()) && u.Mdp.Equals(password)).FirstOrDefault();
    }

    public string GenerateToken(string secret, List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}