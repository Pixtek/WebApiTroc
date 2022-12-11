﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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