using Domain;

namespace Infrastructure.EF;

public class UsersRepository : IUsers
{
    private readonly TrocContextProvider _trocContextProvider;

    public UsersRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<Users> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Utilisateurs.ToList();
    }

    public Users Create(string email, string pseudo, string localite, string mdp)
    {
        using var context = _trocContextProvider.NewContext();
        var user = new Users()
        {
            Email = email,
            Pseudo = pseudo,
            Localite = localite,
            Mdp = mdp
        };
        context.Utilisateurs.Add(user);
        context.SaveChanges();
        return user;
    }
}