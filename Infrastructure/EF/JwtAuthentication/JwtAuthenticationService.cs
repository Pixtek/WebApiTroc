using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.User;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.EF.JwtAuthentication;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IUsers _users;
    //SIMULATION BASE DE DONNEES VITE FAIT POUR TESTER
    private readonly IEnumerable<DbUser> utilisateurs = new List<DbUser>();
    
    public JwtAuthenticationService(IUsers users)
    {
        _users = users;
        utilisateurs = _users.GetAll();
    }


    public DbUser Authenticate(string email, string password)
    {
        //ACCEDER LA BASE DE DONNEES ICI
        
        return utilisateurs.Where(u => u.Email.ToUpper().Equals(email.ToUpper()) && u.Mdp.Equals(password)).FirstOrDefault();
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