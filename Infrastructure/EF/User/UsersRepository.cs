
using Infrastructure.EF.DbEntities;

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

    public DbUser Create(string email, string pseudo, string localite, string mdp)
    {
        using var context = _trocContextProvider.NewContext();
        var user2 = context.Utilisateurs.FirstOrDefault(u => u.Email.Equals(email) || u.Pseudo.Equals(pseudo));
        if (user2 != null)
        {
            throw new KeyNotFoundException($"User with pseudo {pseudo} AND/OR email {email} exist");
        }
        
        var user = new DbUser { Email = email, Pseudo = pseudo, Localite = localite,Mdp = mdp};
        
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
}