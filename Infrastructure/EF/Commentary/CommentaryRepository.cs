using Infrastructure.EF.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Commentary;

public class CommentaryRepository : ICommentary
{
    
    private readonly TrocContextProvider _trocContextProvider;

    public CommentaryRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }
    

    public IEnumerable<DbCommentary> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Commentary.ToList();
    }

    public DbCommentary Create(short note, string nom, string message, int id_User)
    {
        using var context = _trocContextProvider.NewContext();
        var commantary = new DbCommentary()
        {
            Note = note,
            Nom = nom,
            Message = message,
            Id_User = id_User,
           
        };
        context.Commentary.Add(commantary);
        context.SaveChanges();
        return commantary;
    }

    public DbCommentary FetchByName(string name)
    {
        using var context = _trocContextProvider.NewContext();
        var commentary = context.Commentary.FirstOrDefault(a => a.Nom == name);

        if (commentary == null) throw new KeyNotFoundException($"Commentary with name {name} has not been found");
        return commentary;
    }

    public bool Update(DbCommentary dbCommentary)
    {
        using var context = _trocContextProvider.NewContext();

        try
        {
            context.Update(dbCommentary);
            return context.SaveChanges() == 1;

        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        using var context = _trocContextProvider.NewContext();
        try
        {
            context.Remove(new DbCommentary() { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
}