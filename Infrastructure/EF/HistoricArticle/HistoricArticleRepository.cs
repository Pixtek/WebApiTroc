using Infrastructure.EF.Article;
using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.HistoricArticle;

public class HistoricArticle : IHistoricArticle
{
    private readonly TrocContextProvider _trocContextProvider;
    private readonly IArticle _article;

    public HistoricArticle(TrocContextProvider trocContextProvider, IArticle article)
    {
        _trocContextProvider = trocContextProvider;
        _article = article;
    }

    public IEnumerable<DbHistoricArticle> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.HistoricArticles.ToList();
    }

    public DbHistoricArticle archiveArticle(int idArticle)
    {
        using var context = _trocContextProvider.NewContext();
        DbArticle article = _article.FetchById(idArticle);
        
        var historicArticle = new DbHistoricArticle()
        {
            Id = article.Id,
            IdUser = article.IdUser,
            CategoryName = article.CategoryName,
            Name = article.Name,
            URLImage = article.URLImage,
            PublicationDate = article.PublicationDate,
            description = article.description
        };
        context.HistoricArticles.Add(historicArticle);
        context.SaveChanges();

        _article.Delete(article.Id);
        return historicArticle;

    }

    public DbHistoricArticle FetchById(int id)
    {
        using var context = _trocContextProvider.NewContext();
        var article = context.HistoricArticles.FirstOrDefault(a => a.Id == id);

        if (article == null) throw new KeyNotFoundException($"Article with id {id} has not been found");
        return article;
    }
}