using Infrastructure.EF.DbEntities;

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

    public DbCommentary Create(int note, string nom, string message, int id_User)
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
}