using Domain;

namespace Infrastructure.EF;

public class ArticleRepository
{
    private readonly TrocContextProvider _trocContextProvider;

    public ArticleRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<Article> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Articles.ToList();
    }
}