
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.User;


public class UsersRepository : IUsers
{
    private readonly TrocContextProvider _trocContextProvider;

    public UsersRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<DbUser> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Utilisateurs.ToList();
    }

    public DbUser Create(string email, string pseudo, string localite, string mdp, bool isAdmin)
    {
        using var context = _trocContextProvider.NewContext();
        
        var user2 = context.Utilisateurs.FirstOrDefault(u => u.Email.Equals(email) || u.Pseudo.Equals(pseudo));
        if (user2 != null)
        {
            throw new KeyNotFoundException($"L'utilisateur avec le pseudo {pseudo} ET/OU l'email {email} existe deja");
        }
        
        Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$"); 

        if (!validateEmailRegex.IsMatch(email))
        {
            throw new SyntaxErrorException($"L'Email {email} ne respecte pas le bon format");
        }
        
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(mdp))
        {
            throw new SyntaxErrorException($"Le mot de passe doit contenir au moins une minuscule");
        }
        else if (!hasUpperChar.IsMatch(mdp))
        {
            throw new SyntaxErrorException($"Le mot de passe doit contenir au moins une majuscule");
        }
        else if (!hasMiniMaxChars.IsMatch(mdp))
        {
            throw new SyntaxErrorException($"Le mot de passe doit contenir entre 8 et 15 caractères");
        }
        else if (!hasNumber.IsMatch(mdp))
        {
            throw new SyntaxErrorException($"Le mot de passe doit contenir au moins un chiffre");
        }

        else if (!hasSymbols.IsMatch(mdp))
        {
            throw new SyntaxErrorException($"Le mot de passe doit contenir au moins un caractère spéciale");
        }

        
        var hasMiniChars = new Regex(@".{4,}");
        
        if (!hasMiniChars.IsMatch(pseudo))
        {
            throw new SyntaxErrorException($"Le pseudo doit contenir au moins 4 caractères");
        }

        var user = new DbUser { Email = email, Pseudo = pseudo, Localite = localite,Mdp = HashPassword(mdp), admin = isAdmin};
            
        
        context.Utilisateurs.Add(user);
        context.SaveChanges();
        return user;
    }

    public DbUser FetchById(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var user = context.Utilisateurs.FirstOrDefault(u => u.Id == id);

        if (user == null) throw new KeyNotFoundException($"User with id {id} has not been found");

        return user;
    }

    public DbUser FetchByPseudo(string pseudo)
    {
        using var context = _trocContextProvider.NewContext();
        var user = context.Utilisateurs.FirstOrDefault(u => u.Pseudo == pseudo);

        if (user == null) throw new KeyNotFoundException($"User with pseudo {pseudo} has not been found");

        return user;
    }

    public bool Delete(int id)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            context.Remove(new DbUser() { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
    
    public bool Update(String email, String pseudo, String localite, int id)
    { 
        using var context = _trocContextProvider.NewContext();
       
        try
        {
            var user = context.Utilisateurs.First(a => a.Id == id);
            user.Email = email;
            user.Localite = localite;
            user.Pseudo = pseudo;

            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool SetAdmin(bool admin, int id)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            var user = context.Utilisateurs.First(a => a.Id == id);
            user.admin = admin;
            
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
        
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