using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.EF.JwtAuthentication;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IUsers _users;
    private readonly IConfiguration _config;

    private readonly IEnumerable<DbUser> utilisateurs = new List<DbUser>();
    
    public JwtAuthenticationService(IUsers users, IConfiguration config)
    {
        _users = users;
        _config = config;
        utilisateurs = _users.GetAll();
    }


    public DbUser Authenticate(string email, string password)
    {
        return utilisateurs.Where(u => u.Email.ToUpper().Equals(email.ToUpper()) && VerifyPassword(password, u.Mdp)).FirstOrDefault();
    }

    public string GenerateToken(string secret, List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "https://localhost:7018/",
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
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convertir le mot de passe en tableau d'octets et le passer à la méthode ComputeHash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Créer un StringBuilder pour stocker le hash
            StringBuilder builder = new StringBuilder();

            // Formater chaque octet du hash en hexadécimal et l'ajouter au StringBuilder
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            // Renvoyer le hash sous forme de chaîne de caractères
            return builder.ToString();
        }
    }
    public static bool VerifyPassword(string password, string hash)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Hasher le mot de passe donné
            string passwordHash = HashPassword(password);

            // Vérifier si le hash du mot de passe donné correspond au hash stocké
            return hash == passwordHash;
        }
    }
}