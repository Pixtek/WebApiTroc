using Infrastructure.EF.DbEntities;

namespace Infrastructure.EF.Article;

public class ArticleRepository :IArticle
{
    private readonly TrocContextProvider _trocContextProvider;

    public ArticleRepository(TrocContextProvider trocContextProvider)
    {
        _trocContextProvider = trocContextProvider;
    }

    public IEnumerable<DbArticle> GetAll()
    {
        using var context = _trocContextProvider.NewContext();
        return context.Articles.ToList();
    }

    public DbArticle Create(int idUser,  string name, string urlImage, DateTime publicationDate,string nomCat)
    {
        using var context = _trocContextProvider.NewContext();
        var article = new DbArticle()
        {
            IdUser = idUser,
            CategoryName = nomCat,
            Name = name,
            URLImage = urlImage,
            PublicationDate = publicationDate
        };
        context.Articles.Add(article);
        context.SaveChanges();
        return article;
    }
}